using System.Collections.Generic;

using System.Linq;

using UnityEngine;


public class HexagonGeneration : MonoBehaviour
{
    public GameObject[] hexPrefabs, stonePrefabs, mountainPrefabs, puzzlePrefabs, meteoritePrefabs, fireoilpoolPrefabs, geyserPrefabs;
    public GameObject parent;
    public static int layers = 10;
    public int maxMountains, maxRifts, maxMeteorite, maxFireoilPool, maxGeyser;
    //нужны для Data
    public static List<GameObject> hexagons = new List<GameObject>();
    public static List<GameObject> stones = new List<GameObject>();
    public static List<GameObject> mountains = new List<GameObject>();
    public static List<GameObject> puzzles = new List<GameObject>();
    public static List<GameObject> meteorites = new List<GameObject>();
    public static List<GameObject> fireoilpools = new List<GameObject>();
    public static List<GameObject> geysers = new List<GameObject>();
    private List<GameObject> blockedHexes = new List<GameObject>();

    private HashSet<Vector3> occupiedPos = new HashSet<Vector3>();
    private int seed;
    private int mountainCount, geyserCount, fireoilCount, meteoriteCount;
    private float nearRadius = 1f;
    private float farRadius = 2f;

    private List<GameObject> nearNeighbors = new List<GameObject>();
    private List<GameObject> farNeighbors = new List<GameObject>();

    private const float nearHeightReduction = 1f;
    private const float farHeightReduction = 2f;

    private const float riftMin = -2.0f;
    private const float riftMax = -1.5f;
    private const float mountainMin = 3.0f;
    private const float mountainMax = 4.0f;
    private const float defaultMin = 0.5f;
    private const float defaultMax = 0.8f;

    private const float MaxDifferenceHeight = 0.5f;
    private const float MinDifferenceHeight = 0.1f;

    void Start()
    {

        hexagons.Clear();
        stones.Clear();
        mountains.Clear();
        meteorites.Clear();
        fireoilpools.Clear();
        geysers.Clear();
        puzzles.Clear();
        if (MenuManager.difficulty == 0) { maxMountains = 3; maxRifts = 1; }
        else if (MenuManager.difficulty == 1) { maxMountains = 5; maxRifts = 3; }
        else if (MenuManager.difficulty == 2) { maxMountains = 7; maxRifts = 4; }

        seed = Random.Range(0, hexPrefabs.Length - 1);

        GameObject centerHex = Instantiate(hexPrefabs[seed], Vector3.zero, Quaternion.identity);
        centerHex.transform.SetParent(parent.transform);

        occupiedPos.Add(Vector3.zero);
        hexagons.Add(centerHex);

        GenerateLayers();
        SpawnMountains();

        CreateMeteorite(); 
       
        CreateFireoilPool();
        CreateToxideGeyser();
        blockedHexes.AddRange(hexagons.Take(7)); //добавляем центральные hexagon в заблокированные, чтобы исключить спаун rift в центре 
        for (int rift = 0; rift <= maxRifts; rift++) //Создание нескольких разломов
        {
            CreateRift();
        }
        CreateStones();
        parent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        // GenerateResources(hexagons);
        RandomizeHeights();
        GenerateName();
        AddClickHandlers();
    }

    void AddClickHandlers()
    {
        foreach (GameObject hex in hexagons)
        {
            var clickHandler = hex.AddComponent<HexagonClickHandler>();

            if (hex.GetComponent<HexagonOutline>() == null)
            {
                hex.AddComponent<HexagonOutline>();
            }
        }
    }

    void GenerateName()
    {
        int riftCounter = 1;
        int defaultCounter = 1;

        foreach (var hex in hexagons)
        {
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();
            if (landscape != null)
            {
                if (landscape.rift)
                {
                    hex.name = $"HexagonRift_{riftCounter}";
                    riftCounter++;
                }

                else
                {

                    if (hex.name.Contains("HexagonOne"))
                    {
                        hex.name = $"HexagonOne_{defaultCounter}";
                    }
                    else if (hex.name.Contains("HexagonTwo"))
                    {
                        hex.name = $"HexagonTwo_{defaultCounter}";
                    }
                    defaultCounter++;
                }
            }
        }
    }
    void GenerateLayers()
    {
        for (int layer = 0; layer < layers; layer++)
        {
            List<GameObject> newHexes = new List<GameObject>();

            foreach (var hex in hexagons)
            {
                if (hex != null)
                {
                    SpawnHexes(hex, newHexes);
                }
            }

            hexagons.AddRange(newHexes);
        }
    }

    void SpawnHexes(GameObject centerHex, List<GameObject> newHexes)
    {
        foreach (Transform child in centerHex.transform)
        {
            if (IsOccupied(child.position))
            {
                Hexagon_position logic = child.GetComponent<Hexagon_position>();
                logic.Activate();
                continue;
            }
            else if (child.name.Contains("hexagon_position"))
            {
                occupiedPos.Add(child.position);

                seed = Random.Range(0, hexPrefabs.Length - 1);
                GameObject newHex = Instantiate(hexPrefabs[seed], child.position, Quaternion.identity);
                newHex.transform.SetParent(parent.transform);
                if (newHex != null)
                {
                    newHexes.Add(newHex);

                    Hexagon_position logic = child.GetComponent<Hexagon_position>();
                    logic.Activate();
                }
            }
        }
    }

    void GenerateResources(List<GameObject> hexagons)
    {
        foreach (var hex in hexagons)
        {
            HexagonResources Resources = hex.GetComponent<HexagonResources>();
            if ((Resources.pollution == -1) && (Resources.ResourcesFertility == -1) && (hex.GetComponent<HexagonLandscape>().rift == false)) continue;

            foreach (Transform child in hex.transform)
            {
                Resources.ActivateResources();
            }
        }
    }

    bool IsOccupied(Vector3 pos)
    {
        foreach (var p in occupiedPos)
        {
            if (Vector3.Distance(pos, p) < 0.001f) return true;
        }
        return false;
    }

    void RandomizeHeights()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject hex = parent.transform.GetChild(i).gameObject;
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();

            if (landscape.mountain)
            {
                hex.transform.position = new Vector3(hex.transform.position.x, Random.Range(mountainMin, mountainMax), hex.transform.position.z);
            }
            else if (landscape.rift)
            {
                hex.transform.position = new Vector3(hex.transform.position.x, Random.Range(riftMin, riftMax), hex.transform.position.z);
            }
            else if (nearNeighbors.Contains(hex))
            {
                float height = Random.Range(mountainMin - nearHeightReduction, mountainMax - nearHeightReduction);
                height = Mathf.Max(height, defaultMin);
                hex.transform.position = new Vector3(hex.transform.position.x, height, hex.transform.position.z);
            }
            else if (farNeighbors.Contains(hex))
            {
                float height = Random.Range(mountainMin - farHeightReduction, mountainMax - farHeightReduction);
                height = Mathf.Max(height, defaultMin);
                hex.transform.position = new Vector3(hex.transform.position.x, height, hex.transform.position.z);
            }
            else
            {
                hex.transform.position = new Vector3(hex.transform.position.x, Random.Range(defaultMin, defaultMax), hex.transform.position.z);
            }
        }
    }
    void ControlFreeHex(GameObject hex)
    {
        Collider[] colliders = Physics.OverlapSphere(hex.transform.position, nearRadius);
        foreach (var collider in colliders)
        {
            GameObject neighbour = collider.gameObject;
            if ((neighbour != hex) && (neighbour.GetComponent<HexagonLandscape>().rift == false))
            {
                float neighbourHeight = neighbour.transform.position.y;
                float hexHeight = hex.transform.position.y;
                float heightDifference = Mathf.Abs(hexHeight - neighbourHeight);
                if (heightDifference < MaxDifferenceHeight || heightDifference > MaxDifferenceHeight)
                {
                    float newHeight = hexHeight + Random.Range(-MaxDifferenceHeight, MaxDifferenceHeight);
                    neighbour.transform.position = new Vector3(
                    neighbour.transform.position.x,
                    newHeight,
                    neighbour.transform.position.z
                    );
                }
            }
        }
    }

    void CreateRift()
    {
        GameObject startHex = hexagons[Random.Range(0, hexagons.Count)];
        HexagonLandscape startLandscape = startHex.GetComponent<HexagonLandscape>();
        if (startLandscape != null && startLandscape.mountain) { return; }

        ReplaceWithRift(startHex);

        int steps = layers * 10;
        GameObject currentHex = startHex;

        for (int step = 0; step < steps; step++)
        {
            GameObject nextHex = GetNeighbor(currentHex);
            if (nextHex == null)
            {
                break;
            }

            ReplaceWithRift(nextHex);
            currentHex = nextHex;
        }
    }

    GameObject GetNeighbor(GameObject hex)
    {
        Collider[] colliders = Physics.OverlapSphere(hex.transform.position, nearRadius);
        List<GameObject> neighbors = new List<GameObject>();

        foreach (var collider in colliders)
        {
            GameObject neighbor = collider.gameObject;
            if (neighbor.name.Contains("Hexagon") && !neighbor.name.Contains("HexagonRift") && !blockedHexes.Contains(neighbor))
            {
                HexagonLandscape landscape = neighbor.GetComponent<HexagonLandscape>();

                if (landscape == null || (!landscape.mountain && !HasMountain(neighbor)))
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        if (neighbors.Count > 0)
        {
            return neighbors[Random.Range(0, neighbors.Count)];
        }

        return null;
    }

    void ReplaceWithRift(GameObject hex)
    {
        if (HasMountain(hex))
        {
            return;
        }

        hexagons.Remove(hex);
        Destroy(hex);
        GameObject riftHex = Instantiate(hexPrefabs[2], hex.transform.position, Quaternion.identity);
        riftHex.transform.SetParent(parent.transform);
        HexagonLandscape landscape = riftHex.GetComponent<HexagonLandscape>();
        landscape.ActivateRift();
        hexagons.Add(riftHex);
        BlockHexes(riftHex);
    }

    void BlockHexes(GameObject riftHex)
    {
        Collider[] colliders = Physics.OverlapSphere(riftHex.transform.position, nearRadius);
        foreach (var collider in colliders)
        {
            GameObject neighbor = collider.gameObject;
            if (neighbor.name.Contains("Hexagon") && !neighbor.name.Contains("HexagonRift"))
            {
                int riftCount = CountRifts(neighbor);

                if (riftCount > 3 && !blockedHexes.Contains(neighbor))
                {
                    blockedHexes.Add(neighbor);
                }
            }
        }
    }

    int CountRifts(GameObject hex)
    {
        int count = 0;
        Collider[] colliders = Physics.OverlapSphere(hex.transform.position, nearRadius);
        foreach (var collider in colliders) { if (collider.gameObject.name.Contains("HexagonRift")) { count++; } }
        return count;
    }

    void CreateStones()
    {
        foreach (var hex in hexagons)
        {
            bool hasMountain = HasMountain(hex);
            HexagonLandscape mountain = hex.GetComponent<HexagonLandscape>();
            foreach (Transform child in hex.transform)
            {
                if (child.name.Contains("stone_position") && mountain.IsDefault())
                {
                    int chance = hasMountain ? 60 : 20;
                    if (Random.Range(0, 500) <= 1)//спаун жорика
                    {
                        GameObject jorik = Instantiate(puzzlePrefabs[0], child.position, Quaternion.identity);
                        jorik.transform.SetParent(child.transform);
                        puzzles.Add(jorik);
                        Debug.Log("ЭТО ЖОРИКККК!!!!!");
                    }
                    else
                    {
                        if (Random.Range(0, 100) < chance)
                        {
                            int rotX = Random.Range(0, 6);
                            int rotY = Random.Range(0, 6);
                            int rotZ = Random.Range(0, 6);
                            int[] rotations = { 30, 90, 150, 210, 270, 330 };
                            int stoneIndex = Random.Range(0, stonePrefabs.Length);
                            GameObject stone = Instantiate(stonePrefabs[stoneIndex], child.position, Quaternion.identity);
                            stone.transform.SetParent(child.transform);
                            stones.Add(stone);
                            stone.transform.rotation = Quaternion.Euler(rotations[rotX], rotations[rotY], rotations[rotZ]);
                        }
                    }
                }
            }
        }
    }

    bool HasMountain(GameObject hex)
    {
        Collider[] colliders = Physics.OverlapSphere(hex.transform.position, nearRadius);

        foreach (var collider in colliders)
        {
            HexagonLandscape landscape = collider.gameObject.GetComponent<HexagonLandscape>();
            if (landscape != null && landscape.mountain)
            {
                return true;
            }
        }

        return false;
    }

    void SpawnMountains()
    {

        float minDistanceForMountains = 8.0f;

        foreach (var hex in hexagons)
        {
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();
            if (landscape == null) continue;
            float distanceFromCenter = Vector3.Distance(hex.transform.position, Vector3.zero);

            if (distanceFromCenter < minDistanceForMountains) continue;

            foreach (Transform child in hex.transform)
            {
                if (child.name.Contains("mountain_position") && Random.Range(0, 50) <= 2)
                {
                    if (maxMountains > mountainCount)
                    {
                        GameObject mountain = Instantiate(mountainPrefabs[Random.Range(0, mountainPrefabs.Length)], child.position, Quaternion.identity);
                        mountain.transform.SetParent(child.transform);
                        mountains.Add(mountain);
                        mountainCount += 1;

                        landscape.ActivateMountain();

                        Collider[] nearColliders = Physics.OverlapSphere(mountain.transform.position, nearRadius);
                        foreach (var collider in nearColliders)
                        {
                            GameObject neighbor = collider.gameObject;
                            HexagonLandscape neighborLandscape = neighbor.GetComponent<HexagonLandscape>();

                            if (neighborLandscape != null && !neighborLandscape.mountain && !nearNeighbors.Contains(neighbor))
                            {
                                nearNeighbors.Add(neighbor);
                            }
                        }

                        Collider[] farColliders = Physics.OverlapSphere(mountain.transform.position, farRadius);
                        foreach (var collider in farColliders)
                        {
                            GameObject neighbor = collider.gameObject;
                            HexagonLandscape neighborLandscape = neighbor.GetComponent<HexagonLandscape>();

                            if (neighborLandscape != null && !neighborLandscape.mountain && !nearNeighbors.Contains(neighbor) && !farNeighbors.Contains(neighbor))
                            {
                                farNeighbors.Add(neighbor);
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
    void CreateMeteorite()
    {
        float minDistanceForMeteorite = 8.0f;

        foreach (var hex in hexagons)
        {
            
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();
            if (landscape == null) continue;

            float distanceFromCenter = Vector3.Distance(hex.transform.position, Vector3.zero);
            if (distanceFromCenter < minDistanceForMeteorite) continue;

            foreach (Transform child in hex.transform)
            {
                if (child.name.Contains("meteorite_position") && Random.Range(0, 50) <= 500 && landscape.IsDefault())
                {
                    if (maxMeteorite > meteoriteCount)
                    {
                        meteoriteCount++;
                        landscape.ActivateMeteorite();
                        GameObject Meteorite = Instantiate(meteoritePrefabs[Random.Range(0, meteoritePrefabs.Length)], child.position, Quaternion.identity);
                        Meteorite.transform.SetParent(child.transform);

                        Meteorite.transform.eulerAngles = new Vector3(90, 0, 0);
                        Vector3 MeteorPos = Meteorite.transform.position;
                        Meteorite.transform.position = MeteorPos + new Vector3(0, 0, 0.2f);

                        meteorites.Add(Meteorite);
                        HexagonProfile profile = hex.GetComponent<HexagonProfile>();
                        profile.Obscurrium = 50;
                        break;
                    }
                }
            }
        }
    }

    void CreateFireoilPool()
    {
        float minDistanceForFireoil = 8.0f;

        foreach (var hex in hexagons)
        {
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();
            if (landscape == null) continue;

            float distanceFromCenter = Vector3.Distance(hex.transform.position, Vector3.zero);
            if (distanceFromCenter < minDistanceForFireoil) continue;

            foreach (Transform child in hex.transform)
            {
                if (child.name.Contains("fireoilpool_position") && Random.Range(0, 50) <= 10 && landscape.IsDefault())
                {
                    if (maxFireoilPool > fireoilCount)
                    {
                        fireoilCount++;
                        landscape.ActivateFireoilPool();
                        GameObject Fireoil = Instantiate(fireoilpoolPrefabs[Random.Range(0, fireoilpoolPrefabs.Length)], child.position, Quaternion.identity);
                        Fireoil.transform.SetParent(child.transform);

                        Fireoil.transform.eulerAngles = new Vector3(90, 0, 0);
                        Vector3 IgnPos = Fireoil.transform.position;
                        Fireoil.transform.position = IgnPos + new Vector3(0, 0, 0.2f);

                        fireoilpools.Add(Fireoil);

                        HexagonProfile profile = hex.GetComponent<HexagonProfile>();
                        profile.Ignoleum = 50;
                        break;
                    }
                }
            }
        }
    }

    void CreateToxideGeyser()
    {
        float minDistanceForGeyser = 8.0f;

        foreach (var hex in hexagons)
        {
            HexagonLandscape landscape = hex.GetComponent<HexagonLandscape>();
            if (landscape == null) continue;

            float distanceFromCenter = Vector3.Distance(hex.transform.position, Vector3.zero);
            if (distanceFromCenter < minDistanceForGeyser) continue;

            foreach (Transform child in hex.transform)
            {
                if (child.name.Contains("geyser_position") && Random.Range(0, 50) <= 10 && landscape.IsDefault())
                {
                    if (maxGeyser > geyserCount)
                    {
                        geyserCount++;
                        landscape.ActivateGeyser();
                        GameObject Geyser = Instantiate(geyserPrefabs[Random.Range(0, geyserPrefabs.Length)], child.position, Quaternion.identity);
                        Geyser.transform.SetParent(child.transform);

                        Geyser.transform.eulerAngles = new Vector3(90, 0, 0);
                        Vector3 VenPos = Geyser.transform.position;
                        Geyser.transform.position = VenPos + new Vector3(0, 0, 0.3f);

                        geysers.Add(Geyser);
                        HexagonProfile profile = hex.GetComponent<HexagonProfile>();
                        profile.Venesum = 50;
                        break;
                    }
                }
            }
        }
    }
}

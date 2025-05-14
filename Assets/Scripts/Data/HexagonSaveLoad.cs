using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class HexagonSaveLoad : MonoBehaviour
{
    public GameObject[] hexPrefabs, stonePrefabs, mountainPrefabs, puzzlePrefabs, meteoritePrefabs, fireoilpoolPrefabs, geyserPrefabs;
    public GameObject parent;
    public GameObject MenuCanvas, QuotaCanvas;
    public static bool menuOff0rOn = false, quota_menu = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { menuOff0rOn = !menuOff0rOn; }
        if (Input.GetKeyDown(KeyCode.Q)) { quota_menu = !quota_menu; }
        MenuCanvas.SetActive(menuOff0rOn);
        QuotaCanvas.SetActive(quota_menu);
    }
    public void SaveGame()
    {
        SaveMap("map_data.json");
        Debug.Log("���� ���������!");
    }
    public void LoadGame()
    {
        LoadMap("map_data.json");
        Debug.Log("���� ���������!");
    }
    public void ResumeGame()
    {
        menuOff0rOn = false;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }


    public void SaveMap(string fileName)
    {
        MapData mapData = new MapData();

        foreach (var hex in HexagonGeneration.hexagons)
        {
            if (hex == null) continue;

            HexagonProfile profile = hex.GetComponent<HexagonProfile>();

            HexagonData hexData = new HexagonData
            {
                position = hex.transform.position,
                rotation = hex.transform.rotation.eulerAngles,
                prefabIndex = GetPrefabIndex(hex),
                height = hex.transform.position.y,

                obscurrium = profile != null ? profile.Obscurrium : 0f,
                ignoleum = profile != null ? profile.Ignoleum : 0f,
                venesum = profile != null ? profile.Venesum : 0f,
                finibus = profile != null ? profile.Finibus : 0f,
                pollutionLevel = profile != null ? profile.pollutionLevel : 0
            };

            mapData.hexagons.Add(hexData);
        }

        foreach (var mountain in HexagonGeneration.mountains)
        {
            if (mountain == null) continue;
            MountainData mountainData = new MountainData
            {
                position = mountain.transform.position,
                rotation = mountain.transform.rotation.eulerAngles,
                prefabIndex = GetMountainPrefabIndex(mountain)
            };
            mapData.mountains.Add(mountainData);
        }

        foreach (var meteorite in HexagonGeneration.meteorites)
        {
            if (meteorite == null) continue;
            MeteoriteData meteoriteData = new MeteoriteData
            {
                position = meteorite.transform.position,
                rotation = meteorite.transform.rotation.eulerAngles,
                prefabIndex = GetMeteoritePrefabIndex(meteorite)
            };
            mapData.meteorites.Add(meteoriteData);
        }

        foreach (var fireoilpool in HexagonGeneration.fireoilpools)
        {
            if (fireoilpool == null) continue;
            FireOilPoolData fireoilpoolData = new FireOilPoolData
            {
                position = fireoilpool.transform.position,
                rotation = fireoilpool.transform.rotation.eulerAngles,
                prefabIndex = GetFireOilPoolPrefabIndex(fireoilpool)
            };
            mapData.fireoilpools.Add(fireoilpoolData);
        }

        foreach (var geyser in HexagonGeneration.geysers)
        {
            if (geyser == null) continue;
            GeyserData geyserData = new GeyserData
            {
                position = geyser.transform.position,
                rotation = geyser.transform.rotation.eulerAngles,
                prefabIndex = GetGeyserPrefabIndex(geyser)
            };
            mapData.geysers.Add(geyserData);
        }

        foreach (var stone in HexagonGeneration.stones)
        {
            if (stone == null) continue;
            StoneData stoneData = new StoneData
            {
                position = stone.transform.position,
                rotation = stone.transform.rotation.eulerAngles,
                prefabIndex = GetStonePrefabIndex(stone)
            };
            mapData.stones.Add(stoneData);
        }

        foreach (var puzzle in HexagonGeneration.puzzles)
        {
            if (puzzle == null) continue;
            PuzzleData puzzleData = new PuzzleData
            {
                position = puzzle.transform.position,
                rotation = puzzle.transform.rotation.eulerAngles,
                prefabIndex = GetPuzzlesPrefabIndex(puzzle)
            };
            mapData.puzzles.Add(puzzleData);
        }

        // ����������� ������ � JSON � ��������� � ����
        string json = JsonUtility.ToJson(mapData);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log($"����� ��������� � ����: {path}");
    }

    public void LoadMap(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"���� �� ������: {path}");
            return;
        }

        ClearOldMap();

        string json = File.ReadAllText(path);
        MapData mapData = JsonUtility.FromJson<MapData>(json);

        foreach (var hexData in mapData.hexagons)
        {
            GameObject hex = Instantiate(
                hexPrefabs[hexData.prefabIndex],
                hexData.position,
                Quaternion.Euler(hexData.rotation)
            );
            hex.transform.SetParent(parent.transform);
            SetHexagonHeight(hex, hexData.height);

            HexagonProfile profile = hex.GetComponent<HexagonProfile>();
            if (profile != null)
            {
                profile.Obscurrium = hexData.obscurrium;
                profile.Ignoleum = hexData.ignoleum;
                profile.Venesum = hexData.venesum;
                profile.Finibus = hexData.finibus;
                profile.pollutionLevel = hexData.pollutionLevel;
            }

            HexagonGeneration.hexagons.Add(hex);
        }

        foreach (var mountainData in mapData.mountains)
        {
            GameObject mountain = Instantiate(
                mountainPrefabs[mountainData.prefabIndex],
                mountainData.position,
                Quaternion.Euler(mountainData.rotation)
            );
            mountain.transform.SetParent(parent.transform);
            HexagonGeneration.mountains.Add(mountain);
        }


        foreach (var meteoriteData in mapData.meteorites)
        {
            GameObject meteorite = Instantiate(
                meteoritePrefabs[meteoriteData.prefabIndex],
                meteoriteData.position,
                Quaternion.Euler(meteoriteData.rotation)
            );
            meteorite.transform.SetParent(parent.transform);
            HexagonGeneration.meteorites.Add(meteorite);
        }


        foreach (var fireoilpoolData in mapData.fireoilpools)
        {
            GameObject fireoilpool = Instantiate(
                fireoilpoolPrefabs[fireoilpoolData.prefabIndex],
                fireoilpoolData.position,
                Quaternion.Euler(fireoilpoolData.rotation)
            );
            fireoilpool.transform.SetParent(parent.transform);
            HexagonGeneration.fireoilpools.Add(fireoilpool);
        }


        foreach (var geyserData in mapData.geysers)
        {
            GameObject geyser = Instantiate(
                geyserPrefabs[geyserData.prefabIndex],
                geyserData.position,
                Quaternion.Euler(geyserData.rotation)
            );
            geyser.transform.SetParent(parent.transform);
            HexagonGeneration.geysers.Add(geyser);
        }


        foreach (var stoneData in mapData.stones)
        {
            GameObject stone = Instantiate(
                stonePrefabs[stoneData.prefabIndex],
                stoneData.position,
                Quaternion.Euler(stoneData.rotation)
            );
            stone.transform.SetParent(parent.transform);
            HexagonGeneration.stones.Add(stone);
        }

        foreach (var puzzleData in mapData.puzzles)
        {
            GameObject puzzle = Instantiate(
                puzzlePrefabs[puzzleData.prefabIndex],
                puzzleData.position,
                Quaternion.Euler(puzzleData.rotation)
            );
            puzzle.transform.SetParent(parent.transform);
            HexagonGeneration.puzzles.Add(puzzle);
        }

        Debug.Log("����� ������� ���������.");
    }
    private void ClearOldMap()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }


        HexagonGeneration.hexagons.Clear();
        HexagonGeneration.stones.Clear();
        HexagonGeneration.mountains.Clear();
        HexagonGeneration.meteorites.Clear();
        HexagonGeneration.fireoilpools.Clear();
        HexagonGeneration.geysers.Clear();
        HexagonGeneration.puzzles.Clear();

    }

    private int GetPrefabIndex(GameObject hex)
    {
        for (int i = 0; i < hexPrefabs.Length; i++)
        {
            if (hex.name.Contains(hexPrefabs[i].name)) return i;
        }
        return -1;
    }
    private void SetHexagonHeight(GameObject hex, float height)
    {
        hex.transform.position = new Vector3(hex.transform.position.x, height, hex.transform.position.z);
    }

    private int GetStonePrefabIndex(GameObject stone)
    {
        string cleanedName = stone.name.Replace("(Clone)", "").Trim();
        for (int i = 0; i < stonePrefabs.Length; i++) { if (cleanedName == stonePrefabs[i].name) { return i; } }
        return -1;
    }

    private int GetMountainPrefabIndex(GameObject mountain)
    {
        for (int i = 0; i < mountainPrefabs.Length; i++)
        {
            if (mountain.name.Contains(mountainPrefabs[i].name)) return i;
        }
        return -1;
    }

    private int GetMeteoritePrefabIndex(GameObject meteorite)
    {
        for (int i = 0; i < meteoritePrefabs.Length; i++)
        {
            if (meteorite.name.Contains(meteoritePrefabs[i].name)) return i;
        }
        return -1;
    }

    private int GetFireOilPoolPrefabIndex(GameObject fireoilpool)
    {
        for (int i = 0; i < fireoilpoolPrefabs.Length; i++)
        {
            if (fireoilpool.name.Contains(fireoilpoolPrefabs[i].name)) return i;
        }
        return -1;
    }

    private int GetGeyserPrefabIndex(GameObject geyser)
    {
        for (int i = 0; i < geyserPrefabs.Length; i++)
        {
            if (geyser.name.Contains(geyserPrefabs[i].name)) return i;
        }
        return -1;
    }

    private int GetPuzzlesPrefabIndex(GameObject geyser)
    {
        for (int i = 0; i < puzzlePrefabs.Length; i++)
        {
            if (geyser.name.Contains(puzzlePrefabs[i].name)) return i;
        }
        return -1;
    }
}
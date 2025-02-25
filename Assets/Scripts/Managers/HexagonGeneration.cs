using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public GameObject[] prefab_hexagon, prefab_stone, prefab_mountain; // ������� ��� ���������
    public int layers = 1; // ���������� �����
    public GameObject parentObject; // �������� ��� ���������������
    private HashSet<GameObject> prefab_hexagon_instantiate = new HashSet<GameObject>(); // ��������� �������
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // ������� �������
    private int random_seed, random_seed_stone, random_seed_mountain;
    void Start()
    {
        if (prefab_hexagon == null || prefab_hexagon.Length == 0)
        {
            Debug.LogError("��� ��������!");
            return;
        }

        random_seed = Random.Range(0, prefab_hexagon.Length);

        GameObject centralHexagon = Instantiate(prefab_hexagon[random_seed], Vector3.zero, Quaternion.identity);
        centralHexagon.transform.SetParent(parentObject.transform);
        if (centralHexagon == null)
        {
            Debug.LogError("�� ������� ����������� ����!");
            return;
        }

        occupiedPositions.Add(Vector3.zero);
        prefab_hexagon_instantiate.Add(centralHexagon);

        GenerateLayers();
        foreach (Transform child in transform)
        {
            if (child.name.Contains("HexagonRift"))
            {
                int random_seed_rotation = Random.Range(0, 6);
                int[] rotation = { 60, 120, 180, 240, 300, 360 };
                child.localRotation = Quaternion.Euler(child.transform.rotation.x, child.transform.rotation.y, rotation[random_seed_rotation]);

            }
        }
        parentObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        //foreach (Transform child in transform)
        //{
        //    if (child.name.Contains("HexagonRift"))
        //    {
        //        int random_seed_rotation = Random.Range(0, 6);
        //        int[] rotation = { 30, 90, 150, 210, 270, 330 };
        //        child.localRotation = Quaternion.Euler(0, 0, rotation[random_seed_rotation]);
        //    }
        //}

        // ��������� ������ ���� ��� ����� ���������
        RandomizehexagonHeight();
    }

    void GenerateLayers()
    {
        for (int layer = 0; layer < layers; layer++)
        {
            List<GameObject> newHexagons = new List<GameObject>();

            foreach (var sot in prefab_hexagon_instantiate)
            {
                if (sot != null)
                {
                    spawnPrefabs(sot, newHexagons);
                }
            }

            prefab_hexagon_instantiate.Clear();
            prefab_hexagon_instantiate.UnionWith(newHexagons);
        }
    }

    void spawnPrefabs(GameObject centralHexagon, List<GameObject> newHexagons)
    {
        if (centralHexagon == null) return;

        Transform centralTransform = centralHexagon.transform;
        int mountain_is_spawn = 0; // 0 - ��� �� �����, �� ���� | 1 - ���� ������ | 2 - ���� ����
        foreach (Transform child in centralTransform)
        {
            random_seed_stone = Random.Range(0,10); // ���������� ����������� ��� ������ ������
            
            if (child.name.Contains("stone_position") && ( mountain_is_spawn == 0  || mountain_is_spawn == 1))
            {
                if (random_seed_stone >= 3)
                {
                    int random_seed_rotation_x = Random.Range(0, 6);
                    int random_seed_rotation_y = Random.Range(0, 6);
                    int random_seed_rotation_z = Random.Range(0, 6);

                    int[] rotation = { 30, 90, 150, 210, 270, 330 };
                    mountain_is_spawn = 1;
                    random_seed_stone = Random.Range(0, prefab_hexagon.Length);
                    GameObject spawnedObjectStone = Instantiate(prefab_stone[random_seed_stone], child.position, Quaternion.identity);
                    spawnedObjectStone.transform.SetParent(child.transform);
                    spawnedObjectStone.transform.rotation = Quaternion.Euler(rotation[random_seed_rotation_x], rotation[random_seed_rotation_y], rotation[random_seed_rotation_z]);

                    Debug.Log("������ ������ �� �����������:" + spawnedObjectStone.transform.position);
                }
            }
            else if (child.name.Contains("mountain_position") && random_seed_stone<2 && (mountain_is_spawn == 0 || mountain_is_spawn == 2))
            {
                mountain_is_spawn = 2;
                random_seed_mountain = Random.Range(0, prefab_mountain.Length);
                GameObject spawnedObjectmountain = Instantiate(prefab_mountain[random_seed_mountain], child.position, Quaternion.identity);
                spawnedObjectmountain.transform.SetParent(child.transform);
                Debug.Log("������ ������ �� �����������:" + spawnedObjectmountain.transform.position);
            }
            else
            {
                
                if (IsPositionOccupied(child.position))
                {
                    Debug.LogWarning($"������� ������: {child.position}");
                    continue;
                }
                else if (child.name.Contains("hexagon_position"))
                {
                    occupiedPositions.Add(child.position);
                    
                    random_seed = Random.Range(0, prefab_hexagon.Length);
                    if (prefab_hexagon[random_seed].name.Contains("HexagonRift"))
                    {
                        random_seed = Random.Range(0, prefab_hexagon.Length);
                    }
                    GameObject spawnedObjectHexagon = Instantiate(prefab_hexagon[random_seed], child.position, Quaternion.identity);
                    spawnedObjectHexagon.transform.SetParent(parentObject.transform);

                    //if (spawnedObjectHexagon.name.Contains("HexagonRift"))
                    //{
                    //    int random_seed_rotation = Random.Range(0, 6);
                    //    int[] rotation = { 30, 90, 150, 210, 270, 330 };
                    //    spawnedObjectHexagon.transform.rotation = Quaternion.Euler(spawnedObjectHexagon.transform.rotation.x,spawnedObjectHexagon.transform.rotation.y, rotation[random_seed_rotation]);
                    //}
                    if (spawnedObjectHexagon != null)
                    {
                        newHexagons.Add(spawnedObjectHexagon);

                        Hexagon_position logic = child.GetComponent<Hexagon_position>();
                        if (logic != null) logic.hexagon_activate();
                        else Debug.LogError("��� ���������� hexagon_position_logic!");
                    }
                }
            }
        }
    }

    bool IsPositionOccupied(Vector3 position) // �� �����, ������ ��� ���� ;)
    {
        foreach (var pos in occupiedPositions)
        {
            if (Vector3.Distance(position, pos) < 0.001f) return true;
        }
        return false;
    }

    void RandomizehexagonHeight()
    {
        // ���������� ��� �������� ������� ��������
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            GameObject hexagon = parentObject.transform.GetChild(i).gameObject;
            float randomHeight = Random.Range(-1, 0.5f); // ��������� �������� ������
            float randomHeightTime = Random.Range(-randomHeight, randomHeight);
            hexagon.transform.position = new Vector3(
                hexagon.transform.position.x,
                hexagon.transform.position.y + randomHeightTime, // �������� ������ (��� Y)
                hexagon.transform.position.z
            );
        }
    }
}
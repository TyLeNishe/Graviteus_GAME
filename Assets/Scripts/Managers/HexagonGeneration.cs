using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public GameObject[] prefab_hexagon, prefab_stone, prefab_mountain; // Префабы для генерации
    public int layers = 1; // Количество слоев
    public GameObject parentObject; // Родитель для шестиугольников
    private HashSet<GameObject> prefab_hexagon_instantiate = new HashSet<GameObject>(); // Созданные объекты
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // Занятые позиции
    private int random_seed, random_seed_stone, random_seed_mountain;
    void Start()
    {
        if (prefab_hexagon == null || prefab_hexagon.Length == 0)
        {
            Debug.LogError("Нет префабов!");
            return;
        }

        random_seed = Random.Range(0, prefab_hexagon.Length);

        GameObject centralHexagon = Instantiate(prefab_hexagon[random_seed], Vector3.zero, Quaternion.identity);
        centralHexagon.transform.SetParent(parentObject.transform);
        if (centralHexagon == null)
        {
            Debug.LogError("Не создана центральная сота!");
            return;
        }

        occupiedPositions.Add(Vector3.zero);
        prefab_hexagon_instantiate.Add(centralHexagon);

        GenerateLayers();
        parentObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Изменение высоты всех сот после генерации
        RandomizeSotHeight();
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

        foreach (Transform child in centralTransform)
        {
            random_seed_stone = Random.Range(0,10); // генерируем вероятность для спауна камней
            int mountain_is_spawn = 0; // 0 - нет ни камня, ни горы | 1 - есть камень | 2 - есть гора
            if (child.name.Contains("stone_position") && ( mountain_is_spawn == 0  || mountain_is_spawn == 1))
            {
                if (random_seed_stone >= 6)
                {
                    mountain_is_spawn = 1;
                    random_seed_stone = Random.Range(0, prefab_hexagon.Length);
                    GameObject spawnedObjectStone = Instantiate(prefab_stone[random_seed_stone], child.position, Quaternion.identity);
                    spawnedObjectStone.transform.SetParent(child.transform);
                    Debug.Log("Создан камень на координатах:" + spawnedObjectStone.transform.position);
                }
            }
            else if (child.name.Contains("mountain_position") && random_seed_stone<6 && (mountain_is_spawn == 0 || mountain_is_spawn == 2))
            {
                mountain_is_spawn = 2;
                random_seed_mountain = Random.Range(0, prefab_mountain.Length);
                GameObject spawnedObjectmountain = Instantiate(prefab_mountain[random_seed_mountain], child.position, Quaternion.identity);
                spawnedObjectmountain.transform.SetParent(child.transform);
                Debug.Log("Создан камень на координатах:" + spawnedObjectmountain.transform.position);
            }
            else
            {
                if (IsPositionOccupied(child.position))
                {
                    Debug.LogWarning($"Позиция занята: {child.position}");
                    continue;
                }
                else if (child.name.Contains("hexagon_position"))
                {
                    occupiedPositions.Add(child.position);

                    random_seed = Random.Range(0, prefab_hexagon.Length);
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
                        else Debug.LogError("Нет компонента hexagon_position_logic!");
                    }
                }
            }
        }
    }

    bool IsPositionOccupied(Vector3 position) // он нажна, потому что надо ;)
    {
        foreach (var pos in occupiedPositions)
        {
            if (Vector3.Distance(position, pos) < 0.001f) return true;
        }
        return false;
    }

    void RandomizeSotHeight()
    {
        // Перебираем все дочерние объекты родителя
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            GameObject sot = parentObject.transform.GetChild(i).gameObject;
            float randomHeight = Random.Range(-1, 1); // Случайное значение высоты
            sot.transform.position = new Vector3(
                sot.transform.position.x,
                sot.transform.position.y + randomHeight, // Изменяем высоту (ось Y)
                sot.transform.position.z
            );
        }
    }
}
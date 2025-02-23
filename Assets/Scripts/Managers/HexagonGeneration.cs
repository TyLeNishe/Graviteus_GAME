using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonGeneration : MonoBehaviour
{
    public GameObject[] prefab_hexagon; // Префабы для генерации
    public int layers = 1; // Количество слоев
    public GameObject parentObject; // Родитель для сот
    private HashSet<GameObject> prefab_hexagon_instantiate = new HashSet<GameObject>(); // Созданные объекты
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // Занятые позиции
    private int random_seed;

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

    void spawnPrefabs(GameObject centralSot, List<GameObject> newHexagons)
    {
        if (centralSot == null) return;

        Transform centralTransform = centralSot.transform;

        foreach (Transform child in centralTransform)
        {
            if (!child.name.Contains("hexagon_position")) continue;

            if (IsPositionOccupied(child.position))
            {
                Debug.LogWarning($"Позиция занята: {child.position}");
                continue;
            }

            occupiedPositions.Add(child.position);

            random_seed = Random.Range(0, prefab_hexagon.Length);
            GameObject spawnedObject = Instantiate(prefab_hexagon[random_seed], child.position, Quaternion.identity);
            spawnedObject.transform.SetParent(parentObject.transform);

            if (spawnedObject != null)
            {
                newHexagons.Add(spawnedObject);

                Hexagon_position logic = child.GetComponent<Hexagon_position>();
                if (logic != null) logic.hexagon_activate();
                else Debug.LogError("Нет компонента hexagon_position_logic!");
            }
        }
    }

    bool IsPositionOccupied(Vector3 position)
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
            int randomHeight = Random.Range(-2, 2); // Случайное значение высоты
            sot.transform.position = new Vector3(
                sot.transform.position.x,
                sot.transform.position.y + randomHeight, // Изменяем высоту (ось Y)
                sot.transform.position.z
            );
        }
    }
}
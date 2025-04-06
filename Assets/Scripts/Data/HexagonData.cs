using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HexagonData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
    public float height;
    public float obscurrium; // Ҹ�����
    public float ignoleum;   // ���������
    public float venesum;    // ������
    public float finibus;    // ���������
    public int pollutionLevel; // ������� ����������� �� 0 �� 5
}

[System.Serializable]
public class StoneData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex; 
}

[System.Serializable]
public class MountainData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
}

[System.Serializable]
public class MeteoriteData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
}

[System.Serializable]
public class FireOilPoolData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
}

[System.Serializable]
public class GeyserData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
}

[System.Serializable]
public class PuzzleData
{
    public Vector3 position;
    public Vector3 rotation;
    public int prefabIndex;
}
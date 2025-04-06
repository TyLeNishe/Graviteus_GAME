using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    // ������ ���������������
    public List<HexagonData> hexagons = new List<HexagonData>();

    // ������ ������
    public List<StoneData> stones = new List<StoneData>();

    // ������ ���
    public List<MountainData> mountains = new List<MountainData>();

    // ������ ����������
    public List<MeteoriteData> meteorites = new List<MeteoriteData>();

    // ������ �������� ���
    public List<FireOilPoolData> fireoilpools = new List<FireOilPoolData>();

    // ������ ��������
    public List<GeyserData> geysers = new List<GeyserData>();

    // ������ ��������
    public List<PuzzleData> puzzles = new List<PuzzleData>();

}

using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    // ������ ���������������
    public List<HexagonData> hexagons = new();

    // ������ ������
    public List<StoneData> stones = new();

    // ������ ���
    public List<MountainData> mountains = new();

    // ������ ����������
    public List<MeteoriteData> meteorites = new();

    // ������ �������� ���
    public List<FireOilPoolData> fireoilpools = new();

    // ������ ��������
    public List<GeyserData> geysers = new();

    // ������ ��������
    public List<PuzzleData> puzzles = new();

}

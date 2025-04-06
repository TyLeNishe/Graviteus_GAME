using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    // Список шестиугольников
    public List<HexagonData> hexagons = new List<HexagonData>();

    // Список камней
    public List<StoneData> stones = new List<StoneData>();

    // Список гор
    public List<MountainData> mountains = new List<MountainData>();

    // Список метеоритов
    public List<MeteoriteData> meteorites = new List<MeteoriteData>();

    // Список нефтяных озёр
    public List<FireOilPoolData> fireoilpools = new List<FireOilPoolData>();

    // Список гейзеров
    public List<GeyserData> geysers = new List<GeyserData>();

    // Список пасхалок
    public List<PuzzleData> puzzles = new List<PuzzleData>();

}

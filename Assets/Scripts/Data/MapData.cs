using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    // Список шестиугольников
    public List<HexagonData> hexagons = new();

    // Список камней
    public List<StoneData> stones = new();

    // Список гор
    public List<MountainData> mountains = new();

    // Список метеоритов
    public List<MeteoriteData> meteorites = new();

    // Список нефтяных озёр
    public List<FireOilPoolData> fireoilpools = new();

    // Список гейзеров
    public List<GeyserData> geysers = new();

    // Список пасхалок
    public List<PuzzleData> puzzles = new();

}

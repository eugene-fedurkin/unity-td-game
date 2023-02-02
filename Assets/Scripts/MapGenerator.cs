using UnityEngine;


public enum MapCellType
{
    Road,
    Grass
}

public class MapGenerator : MonoBehaviour {
    [SerializeField] World world;

    [Header("Map Prefabs")]
    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject grassPrefab;

    Matrix<MapCellType> mapMatrix;
    GameDataManager gameDataManager;

    void Awake() {
        gameDataManager = GameDataManager.instance;
        mapMatrix = world.getLevel(gameDataManager.getLevel()).map;
        generateMap(mapMatrix);
    }

    public Vector2Int getMapSize() {
        return new Vector2Int(mapMatrix.arrays.Count, mapMatrix.arrays[0].cells.Count);
    }

    public MapCellType getCellType(int x, int z) {
        return mapMatrix.arrays[x][z];
    }

    void generateMap(Matrix<MapCellType> matrix) {
        for (int i = 0; i < matrix.arrays.Count; i++) {
            for (int j = 0; j < matrix.arrays[i].cells.Count; j++) {
                generateCell(matrix.arrays[i].cells[j], i, j);
            }
        }
    }

    void generateCell(MapCellType cell, int x, int z) {
        GameObject cellObject = Instantiate(getPrefabByType(cell), new Vector3(x, 0, z), Quaternion.identity);
        cellObject.transform.parent = gameObject.transform;
        
    }

    GameObject getPrefabByType(MapCellType type) {
        switch(type) {
            case MapCellType.Road: return roadPrefab;
            case MapCellType.Grass: return grassPrefab;
        }

        return grassPrefab;
    }
}

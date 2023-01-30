using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapCellType
{
    Road,
    Grass
}

public class MapGenerator : MonoBehaviour
{
    [SerializeField] World world;
    [SerializeField] GameDataManager gameDataManager;

    [Header("Map Prefabs")]
    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject grassPrefab;

    // Start is called before the first frame update
    void Start() {
        Level level = world.getLevel(gameDataManager.getLevel());
        generateMap(level.map);
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

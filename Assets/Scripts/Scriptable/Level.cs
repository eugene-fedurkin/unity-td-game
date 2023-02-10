using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Cells<T> {
    public List<T> cells = new List<T>();
    public T this[int index] => cells[index];
}

[System.Serializable]
public class Matrix<T> {
    public List<Cells<T>> arrays = new List<Cells<T>>();
    public T this[int x, int y] => arrays[x][y];
}

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
public class Level : ScriptableObject {
    public Matrix<MapCellType> map;
    public List<Spawn> spawns;
    public List<PathLink> path;
    public int basePosition;
    public GameObject previewForSelect;
}

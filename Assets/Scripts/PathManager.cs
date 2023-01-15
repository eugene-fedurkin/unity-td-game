using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathLink {
    public Vector3 start;
    public List<Vector3> end;
}

public class PathManager : MonoBehaviour {
    [SerializeField] List<PathLink> path;

    void Start() {
        
    }

    public Vector3 getNextCoordinate(Vector3 coordinate) {
        int currentCoordinateIndex = path.FindIndex(pathLink => pathLink.start.x == coordinate.x && pathLink.start.z == coordinate.z);
        PathLink newPathLink = currentCoordinateIndex > -1 && path[currentCoordinateIndex] != null ? path[currentCoordinateIndex] : null;

        if (newPathLink != null) {
            return getRandom(newPathLink.end);
        }

        return getRandom(path[0].end);
    }

    Vector3 getRandom(List<Vector3> list) {
        if (list.Count == 1) {
            return list[0];
        }

        return list[Random.Range(0, list.Count)];

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathLink {
    public Vector3 start;
    public List<Vector3> end;
}

public class Coordinates {
    public float x;
    public float z;
    public Coordinates(Vector3 vector) {
        x = vector.x;
        z = vector.z;
    }
}

public class PathManager : MonoBehaviour {
    [SerializeField] List<PathLink> path;

    public Vector3 getStartByIndex(int idx) {
        if (idx > path.Count) {
            return path[path.Count - 1].start;
        }

        return path[idx].start;
    }

    public Vector3 getEndtByIndex(int idx) {
        if (idx > path.Count) {
            return path[path.Count - 1].end[0];
        }

        return path[idx].end[0];
    }

    public Coordinates getNextCoordinate(Vector3 coordinate) {
        int currentCoordinateIndex = path.FindIndex(pathLink => pathLink.start.x == coordinate.x && pathLink.start.z == coordinate.z);
        PathLink newPathLink = currentCoordinateIndex > -1 && path[currentCoordinateIndex] != null ? path[currentCoordinateIndex] : null;

        if (newPathLink != null) {
            return getRandom(newPathLink.end);
        }

        return null;
    }

    Coordinates getRandom(List<Vector3> list) {
        if (list.Count == 1) {
            return new Coordinates(list[0]);
        }

        return new Coordinates(list[Random.Range(0, list.Count)]);

    }
}

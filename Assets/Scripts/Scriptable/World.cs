using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "World")]
public class World : ScriptableObject {
    [SerializeField] List<Level> levels;

    public Level getLevel(int level)
    {
        if (levels.Count < level) {
            return levels[level];
        }

        return levels[0];
    }
}

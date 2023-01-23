using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType {
    Skeleton,
    Bird
}

[System.Serializable]
public class Wave
{
    public float interval;
    public List<UnitType> units;
}

[System.Serializable]
public class Spawn
{
    public Vector3 start;
    public List<Wave> waves;
    public bool destroyed;
}

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<Spawn> spawns;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject unitPrefab;

    int activeSpawnIndex;

    public void activateSpawn(int idx) {
        if (spawns.Count > idx) {
            activeSpawnIndex = idx;
            StartCoroutine(spawnLifeStart(spawns[idx], 0, 0));
        }
    }

    public void initiateSpawns() {
        int idx = 0;
        spawns.ForEach(spawn => {
            GameObject obj = Instantiate(spawnPrefab, spawn.start, Quaternion.identity);
            obj.name = "Spawn " + idx++;
        });
    }

    IEnumerator spawnLifeStart(Spawn spawn, int waveIndex, int unitIndex) {
        if (spawn.waves.Count > waveIndex) {
            yield return new WaitForSeconds(spawn.waves[waveIndex].interval);
            if (spawn.waves[waveIndex].units.Count > unitIndex) {
                Debug.Log("UNIT SPAWNED ->"+ spawn.waves[waveIndex].units[unitIndex]);
                initUnit(spawn.waves[waveIndex].units[unitIndex], spawn.start, "waveIndex " + waveIndex + " unitIndex " + unitIndex);
                StartCoroutine(spawnLifeStart(spawn, waveIndex, unitIndex + 1));
            } else {
                Debug.Log("NEXT WAVE ->");
                StartCoroutine(spawnLifeStart(spawn, waveIndex + 1, 0));
            }
        } else {
            Debug.Log("ALL WAVES ARE Complete");

            activateSpawn(activeSpawnIndex + 1);
        }
    }

    void initUnit(UnitType unitType, Vector3 vector, string name) {
        Instantiate(unitPrefab, vector, Quaternion.identity).name = name;
    }


}

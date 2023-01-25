using System;
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
}

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<Spawn> spawns;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] UnitManager unitManager;

    int activeSpawnIndex;
    int activeWaveIndex;
    bool spawnInProcess;

    private void Start()
    {
        activeSpawnIndex = 0;
        spawnInProcess = false;
    }

    public bool triggerSpawn() {
        if (spawns.Count > activeSpawnIndex) {
            if (spawns[activeSpawnIndex].waves.Count > activeWaveIndex) {
                StartCoroutine(spawnLifeStart(spawns[activeSpawnIndex], activeWaveIndex, 0));

                return true;
            }
        }

        return false;
    }

    public void initiateSpawns() {
        int idx = 0;
        spawns.ForEach(spawn => {
            GameObject obj = Instantiate(spawnPrefab, spawn.start, Quaternion.identity);
            obj.name = "Spawn " + idx++;
        });
    }

    public bool getActiveSpawnInProcess() {
        return spawnInProcess;
    }

    IEnumerator spawnLifeStart(Spawn spawn, int waveIndex, int unitIndex) {
        spawnInProcess = true;
        if (spawn.waves.Count > waveIndex) {
            yield return new WaitForSeconds(spawn.waves[waveIndex].interval);
            if (spawn.waves[waveIndex].units.Count > unitIndex) {
                // Debug.Log("UNIT SPAWNED ->"+ spawn.waves[waveIndex].units[unitIndex]);
                unitManager.initUnit(spawn.waves[waveIndex].units[unitIndex], spawn.start, "waveIndex " + waveIndex + " unitIndex " + unitIndex);
                StartCoroutine(spawnLifeStart(spawn, waveIndex, unitIndex + 1));
            } else {
                spawnInProcess = false;
                activeWaveIndex++;

                if (spawns[activeSpawnIndex].waves.Count <= activeWaveIndex) {
                    activeSpawnIndex++;
                    activeWaveIndex = 0;
                }
                // waveFinish.Invoke();
                //Debug.Log("NEXT WAVE ->");
                //StartCoroutine(spawnLifeStart(spawn, waveIndex + 1, 0));
            }
        } else {
            // Debug.Log("ALL WAVES ARE Complete");
            spawnInProcess = false;
            activeSpawnIndex++;
            activeWaveIndex = 0;

            // activateSpawn(activeSpawnIndex + 1);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum UnitType {
    Skeleton,
    Bird
}

[System.Serializable]
public class Wave {
    public float interval;
    public List<UnitType> units;
}

[System.Serializable]
public class Spawn {
    public int startIndex;
    public List<Wave> waves;
}

public class SpawnManager : MonoBehaviour {
    [SerializeField] List<Spawn> spawns;
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] UnitManager unitManager;
    [SerializeField] PathManager pathManager;

    int activeSpawnIndex = 0;
    int activeWaveIndex = 0;
    bool spawnInProcess = false;
    bool spawnsFinished = false;
    Coroutine currentCo;

    List<GameObject> initedSpawns;

    public bool triggerSpawn() {
        if (spawns.Count > activeSpawnIndex) {
            if (spawns[activeSpawnIndex].waves.Count > activeWaveIndex) {
                currentCo = StartCoroutine(spawnLifeStart(spawns[activeSpawnIndex], activeWaveIndex, 0));

                return true;
            }
        }

        return false;
    }

    public void refreshSpawns() {
        destroyAllSpawns();

        int idx = 0;
        initedSpawns = spawns.ConvertAll(spawn => {
            GameObject obj = Instantiate(spawnPrefab, getPositionByIndex(spawn.startIndex), Quaternion.identity);
            obj.name = "Spawn " + idx++;
            obj.transform.parent = gameObject.transform;

            return obj;
        });
    }

    public bool getActiveSpawnInProcess() {
        return spawnInProcess;
    }

    public bool getSpawnFinished() {
        return spawnsFinished;
    }

    IEnumerator spawnLifeStart(Spawn spawn, int waveIndex, int unitIndex) {
        spawnInProcess = true;
        if (spawn.waves.Count > waveIndex) {
            yield return new WaitForSeconds(spawn.waves[waveIndex].interval);
            if (spawn.waves[waveIndex].units.Count > unitIndex) {
                unitManager.initUnit(spawn.waves[waveIndex].units[unitIndex], getPositionByIndex(spawn.startIndex), "waveIndex " + waveIndex + " unitIndex " + unitIndex);
                currentCo = StartCoroutine(spawnLifeStart(spawn, waveIndex, unitIndex + 1));
            } else {
                spawnInProcess = false;
                activeWaveIndex++;

                if (spawns[activeSpawnIndex].waves.Count <= activeWaveIndex) {
                    activeSpawnIndex++;
                    activeWaveIndex = 0;
                    checkisAllSpawnsFinished();
                }
            }
        } else {
            spawnInProcess = false;
            activeSpawnIndex++;
            activeWaveIndex = 0;
            checkisAllSpawnsFinished();
        }
    }

    void checkisAllSpawnsFinished() {
        if (spawns.Count <= activeSpawnIndex) {
            // GlobalEventManager.allSpawnsFinished();
            spawnsFinished = true;
        }
    }

    Vector3 getPositionByIndex(int idx) {
        Vector3 vector = pathManager.getStartByIndex(idx);

        return new Vector3(vector.x, 1, vector.z);
    }

    void destroyAllSpawns() {
        if (initedSpawns != null) {
            initedSpawns.ForEach(spawn => Destroy(spawn.gameObject));
            initedSpawns = null;
            activeSpawnIndex = 0;
            activeWaveIndex = 0;
            spawnInProcess = false;
            spawnsFinished = false;
        }

        if (currentCo != null) {
            StopCoroutine(currentCo);
            currentCo = null;
        }
    }
}

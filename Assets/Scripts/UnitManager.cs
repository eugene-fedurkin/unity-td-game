using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    [SerializeField] GameObject unitPrefab;
    [SerializeField] SpawnManager spawnManager;

    List<GameObject> units;

    private void Awake() {
        GlobalEventManager.onUnitCreate.AddListener(registerUnit);
        GlobalEventManager.onUnitDeath.AddListener(unregisterUnit);

        units = new List<GameObject>();
    }

    void unregisterUnit(GameObject unit) {
        units.Remove(unit);

        bool inProcess = spawnManager.getActiveSpawnInProcess();

        if (inProcess) {
            return;
        }

        if (units.Count == 0) {
            // END WAVE
            GlobalEventManager.endWave();
        }
    }

    void registerUnit(GameObject unit) {
        units.Add(unit);
    }

    public void initUnit(UnitType unitType, Vector3 vector, string name) {
        // Add deps from unitType
        GameObject obj = Instantiate(unitPrefab, vector, Quaternion.identity);
        obj.name = name;
        UnitBehaviour unit = obj.GetComponent<UnitBehaviour>();
        unit.health = 10;
        unit.speed = 30;
        unit.power = 5;
        unit.gold = 1;
    }
}

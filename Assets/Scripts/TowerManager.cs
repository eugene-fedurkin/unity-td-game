using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    [SerializeField] GameObject bulletPrefab;

    List<Building> towers = new List<Building>();

    private int count = 0;

    public Building createTower(Building building) {
        Building build = Instantiate(building);
        build.transform.parent = gameObject.transform;
        towers.Add(build);
        build.name = count.ToString();

        TowerRange tower = getTower(build);
        tower.name = count.ToString();
        Debug.Log("build.name = " + build.name + "  " + tower.gameObject.name);
        tower.setConfiguration(1,5, 10f, bulletPrefab);
        count++;

        return build;
    }

    public void startTrackUnitFor(Building building) {
        getTower(building).startTrackUnit();
    }

    public void destroyAllTowers() {
        towers.ForEach(t => t.destroy());
        towers.Clear();
    }

    TowerRange getTower(Building building) {
        return building.gameObject.GetComponentInChildren<TowerRange>();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    [SerializeField] GameObject bulletPrefab;

    List<Building> towers = new List<Building>();

    public Building createTower(Building building) {
        Building build = Instantiate(building);
        build.transform.parent = gameObject.transform;
        towers.Add(build);

        TowerRange tower = getTower(building);
        tower.bulletPrefab = bulletPrefab;
        tower.interval = 1;
        tower.damage = 5;
        tower.bulletSpeed = 10f;

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
        return building.GetComponentInChildren<TowerRange>();
    }
}

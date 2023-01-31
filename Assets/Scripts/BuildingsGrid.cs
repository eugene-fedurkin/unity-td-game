using UnityEngine;

public class BuildingsGrid : MonoBehaviour {
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] TowerManager _towerManager;

    readonly Vector3 gridHeight = new Vector3(0, 1.5f, 0);
    bool[,] grid;
    Vector2Int gridSize;
    Building flyingBuilding;
    Camera mainCamera;

    void Start() {
        gridSize = mapGenerator.getMapSize();
        grid = new bool[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
    }
    
    void Update() {
        if (flyingBuilding != null) {
            Plane groundPlane = new Plane(Vector3.up, gridHeight);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position)) {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                bool avalable = true;
                if (x < 0 || x >= gridSize.x || z < 0 || z >= gridSize.y || isPlaceTaken(x, z) || isRoad(x, z)) {
                    avalable = false;
                }

                flyingBuilding.transform.position = new Vector3(x, gridHeight.y, z);
                flyingBuilding.setTransparent(avalable);

                if (avalable && Input.GetMouseButtonDown(0)) {
                    placeFlyingBuilding(x, z);
                }
            }
        }
    }

    public void startPlacingBuilding(Building building) {
        if (flyingBuilding != null) {
            flyingBuilding.destroy();
        }

        flyingBuilding = _towerManager.createTower(building);
    }

    bool isPlaceTaken(int x, int z) {
        return grid[x, z] == true;
    }

    bool isRoad(int x, int z) {
        return mapGenerator.getCellType(x, z) == MapCellType.Road;
    }

    void placeFlyingBuilding(int x, int z) {
        grid[x, z] = true;
        _towerManager.startTrackUnitFor(flyingBuilding);
        flyingBuilding.setNormal();
        flyingBuilding = null;
    }
}

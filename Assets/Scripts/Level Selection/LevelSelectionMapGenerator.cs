using UnityEngine;

public class LevelSelectionMapGenerator : MapSizeGetter {
    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] World world;

    Vector2Int cameraMapRestrict = new Vector2Int(8, 8);

    void Start() {
        generate();
    }

    public override Vector2Int getMapSize() { return cameraMapRestrict; }

    void generate() {
        int level = GameDataManager.instance.getLevel();

        for (int i = 0; i <= 2; i++) {
            generateAreaForLevel(i);
        }
    }

    void generateAreaForLevel(int level) {
        int width = cameraMapRestrict.x;
        int levelPlacement = width / 2;
        int levelPlacementZ = level * width + levelPlacement;

        for (int x = 0; x < width; x++) {
            for (int z = level * width; z < level * width + width; z++) {
                if (x == levelPlacement && z == levelPlacementZ) {
                    initPrefab(world.getLevel(level).previewForSelect, x, 1, z, level);
                } else if (x == levelPlacement) {
                    initPrefab(roadPrefab, x, 0, z, level);
                } else {
                    initPrefab(grassPrefab, x, 0, z, level);
                }

                if (cameraMapRestrict.y < z) {
                    cameraMapRestrict.y = z;
                }
            }
        }
    }

    void initPrefab(GameObject prefab, int x, int y, int z, int level) {
        GameObject tile = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        tile.transform.SetParent(gameObject.transform);
        tile.name = "X" + x + " Z" + z + " Level" + level;
    }
}

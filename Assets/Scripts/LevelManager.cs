using UnityEngine;


public class LevelManager : MonoBehaviour {
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] TowerManager towerManager;
    [SerializeField] BaseManager baseManager;
    [SerializeField] GameObject startWaveButton;
    [SerializeField] GameObject resetButton;

    private void Awake() {
        GlobalEventManager.onEndWave.AddListener(() => startWaveButton.SetActive(true));
        GlobalEventManager.onBaseDeath.AddListener(() => {
            resetButton.SetActive(true);
        });

        GlobalEventManager.onRefreshLevel.AddListener(() => {
            init();
            resetButton.SetActive(false);
        });
    }

    void Start() {
        init();
    }

    public void startWave() {
        bool isActivated = spawnManager.triggerSpawn();

        if (isActivated) {
            startWaveButton.SetActive(false);
        }
    }

    void init() {
        spawnManager.refreshSpawns();
        baseManager.refreshBase();
        towerManager.destroyAllTowers();
    }
}

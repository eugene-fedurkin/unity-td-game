using UnityEngine;


public class LevelManager : MonoBehaviour {
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] TowerManager towerManager;
    [SerializeField] BaseManager baseManager;
    [SerializeField] GameObject startWaveButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] LevelCompleteController levelCompleteWindow;

    private void Awake() {
        GlobalEventManager.onEndWave.AddListener(() => {
            if (spawnManager.getSpawnFinished()) {
                levelCompleteWindow.gameObject.SetActive(true);
            } else {
                startWaveButton.SetActive(true);
            }
        });
        GlobalEventManager.onBaseDeath.AddListener(() => { resetButton.SetActive(true); });
        GlobalEventManager.onRefreshLevel.AddListener(() => {
            init();
            resetButton.SetActive(false);
        });
        // GlobalEventManager.onAllSpawnsFinished.AddListener(() => {
        //     levelCompleteWindow.gameObject.SetActive(true);
        // });
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

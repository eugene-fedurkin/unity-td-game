using TMPro;
using UnityEngine;

public class LevelStatIndicatorController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI levelCounter;
    [SerializeField] TextMeshProUGUI spawnCounter;
    [SerializeField] TextMeshProUGUI waveCounter;
    [SerializeField] SpawnManager spawnManager;

    void Start() {
        refreshData();
        GlobalEventManager.onEndWave.AddListener(() => {
            spawnCounter.text = spawnManager.getSpawnIndex().ToString();
            waveCounter.text = spawnManager.getWaveIndex().ToString();
        });
    }

    void refreshData() {
        GameSession session = GameDataManager.instance.getLastSession();
        levelCounter.text = session?.level.ToString();

        spawnCounter.text = "0";
        waveCounter.text = "0";
    }
}

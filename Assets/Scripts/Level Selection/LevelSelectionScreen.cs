using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionScreen : MonoBehaviour {
    [SerializeField] World world;
    [SerializeField] GameObject listPanel;
    [SerializeField] LevelItem levelPrefab;

    void Start() {
        int currentLevelIndex = GameDataManager.instance.getLevel() + 1;
        int allCount = world.levelsCount();

        for (int levelIndex = 1; levelIndex <= allCount; levelIndex++) {
            LevelItem item = Instantiate(levelPrefab, listPanel.transform.position, Quaternion.identity);
            item.setProps(levelIndex, levelIndex > currentLevelIndex, (level) => {
                if (currentLevelIndex >= level) {
                    GameSession lastSession = GameDataManager.instance.getLastSession();
                    GlobalEventManager.loadScene("Game Scene", lastSession);
                    SceneManager.LoadScene("Game Scene");
                }
            });
            item.transform.parent = listPanel.transform;
        }
    }
}

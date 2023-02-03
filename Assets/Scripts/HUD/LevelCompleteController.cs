using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI levelCounter;
    void Start() {
        int level = GameDataManager.instance.getLastSession()?.level ?? 0;
        levelCounter.text = "Level: " + level.ToString();

        GameDataManager.instance.patchSession(++level);
        GameDataManager.instance.Save();
    }

    public void complete() {
        GlobalEventManager.loadScene("Main Menu Scene", null);
        SceneManager.LoadScene("Main Menu Scene");
    }
}

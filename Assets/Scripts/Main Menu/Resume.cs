using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    [SerializeField] GameDataManager gameDataManager;
    [SerializeField] TextMeshProUGUI dateTimeText;

    private GameSession? lastSession;

    void Start() {
        lastSession = gameDataManager.getLastSession();

        if (lastSession == null) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            dateTimeText.text = lastSession?.lastDatePlayed.ToString("ddd hh:mm");
        }
    }

    public void onClick() {
        GlobalEventManager.loadScene("Game Scene", lastSession);
        SceneManager.LoadScene("Game Scene");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {
    private GameSession focusedSession;
    public void setFocusedSession(GameSession session) {
        focusedSession = session;
    }

    public void click() {
        if (focusedSession != null) {
            GlobalEventManager.loadScene("Game Scene", focusedSession);
            SceneManager.LoadScene("Game Scene");
        }
    }
}

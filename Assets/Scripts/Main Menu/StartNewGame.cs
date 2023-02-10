using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour {
   public void startGame() {
      GlobalEventManager.loadScene("Level Selection Scene", null);
      SceneManager.LoadScene("Level Selection Scene");
   }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
   public void startGame()
   {
      GlobalEventManager.loadScene("Game Scene", null);
      SceneManager.LoadScene("Game Scene");
   }
}

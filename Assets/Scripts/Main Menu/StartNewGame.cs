using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
   public void startGame()
   {
      GlobalEventManager.loadScene("Game Scene");
      SceneManager.LoadScene("Game Scene");
   }
}
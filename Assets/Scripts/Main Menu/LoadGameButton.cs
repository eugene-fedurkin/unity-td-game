using UnityEngine;

public class LoadGameButton : MonoBehaviour
{
    void Start()
    {
        GameDataManager gameDataManager = GameDataManager.instance;
        gameObject.SetActive(gameDataManager.getSavedSessions().Count > 0);
    }
}

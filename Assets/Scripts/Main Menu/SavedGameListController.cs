using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedGameListController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GlobalEventManager globalEventManager;

    void Start()
    {
        GameDataManager gameDataManager = GameDataManager.instance;
        generateList(gameDataManager.getSavedSessions());
    }

    void generateList(List<GameSession> sessions)
    {
        sessions.ForEach(session => {
            GameObject button = Instantiate(buttonPrefab);
            MenuItem menuItem = button.GetComponentInChildren<MenuItem>();
            menuItem.setText("Date: " + session.lastDatePlayed.ToString("ddd hh:mm") + "," + "Level: " + session.level); // TODO: move format to config file
            menuItem.changeTextSize(MenuItemTextSize.Small);
            menuItem.setAction(() =>
            {
                GlobalEventManager.loadScene("Game Scene", session);
                SceneManager.LoadScene("Game Scene");
            });
            button.transform.parent = gameObject.transform;
        });
    }
}

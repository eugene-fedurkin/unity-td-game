using System;
using System.Collections.Generic;
using UnityEngine;

public class SavedGameListController : MonoBehaviour {
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private LoadButton loadButton;

    MenuItem focusedSession;

    void Start() {
        GameDataManager gameDataManager = GameDataManager.instance;
        generateList(gameDataManager.getSavedSessions());
    }

    private void OnDisable() {
        if (focusedSession != null) {
            loadButton.setFocusedSession(null);
            focusedSession.toggleActive(false);
        }
    }

    void generateList(List<GameSession> sessions) {
        sessions.ForEach(session => {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(gameObject.transform);
            MenuItem menuItem = button.GetComponentInChildren<MenuItem>();
            menuItem.setText("Date: " + session.lastDatePlayed.ToString("ddd hh:mm") + "," + "Level: " + session.level); // TODO: move format to config file
            menuItem.changeTextSize(MenuItemTextSize.Small);
            menuItem.setAction(() => {
                if (focusedSession) {
                    focusedSession.toggleActive(false);
                }

                focusedSession = menuItem;
                menuItem.toggleActive(true);
                loadButton.gameObject.SetActive(true);
                loadButton.setFocusedSession(session);
            });
        });
    }
}

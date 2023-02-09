using System;
using TMPro;
using UnityEngine;

public enum MenuItemTextSize {
    Small,
    Medium,
    Large
}

public class MenuItem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI currText;
    [SerializeField] private GameObject activeIndicator;
    private Action menuAction;

    public void toggleActive(bool active) {
        activeIndicator.SetActive(active);
    }

    public void setAction(Action action) {
        menuAction = action;
    }

    public void setText(String text) {
        currText.text = text;
    }

    public void changeTextSize(MenuItemTextSize size) {
        switch (size) {
            case MenuItemTextSize.Small: {
                currText.fontSize = 18;
                return;
            };
            case MenuItemTextSize.Medium: {
                currText.fontSize = 26;
                return;
            }
        };

        currText.fontSize = 34;
    }

    public void onClick() {
        if (menuAction != null) {
            menuAction();
        }
    }
}

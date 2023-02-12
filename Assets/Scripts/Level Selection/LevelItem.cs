using System;
using TMPro;
using UnityEngine;

public class LevelItem : MonoBehaviour {
    [SerializeField] GameObject lockIcon;
    [SerializeField] TMP_Text text;

    int level;
    bool isLock;
    Action<int> action;

    private void Start() {
        lockIcon.SetActive(isLock);
        text.text = isLock ? "" : level.ToString();
    }

    public void setProps(int level, bool isLock, Action<int> action) {
        this.level = level;
        this.isLock = isLock;
        this.action = action;
    }

    public void click() {
        if (action != null) {
            action(level);
        }
    }
}

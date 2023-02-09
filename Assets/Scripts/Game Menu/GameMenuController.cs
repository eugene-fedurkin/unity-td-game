using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour {
    [SerializeField] GameObject menuList;

    private void Start() {
        menuList.SetActive(false);
    }

    public void toggleMenu() {
        menuList.SetActive(!menuList.activeSelf);
    }

    public void openMainMenu() {
        GlobalEventManager.loadScene("Main Menu Scene", null);
        SceneManager.LoadScene("Main Menu Scene");
    }
}

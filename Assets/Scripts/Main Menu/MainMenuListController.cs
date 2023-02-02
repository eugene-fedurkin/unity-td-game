using System;
using UnityEngine;

public class MainMenuListController : MonoBehaviour
{
    [SerializeField] private GameObject mainList;
    [SerializeField] private GameObject savesList;
    [SerializeField] private GameObject backButton;

    private void Start()
    {
        mainList.SetActive(true);
        savesList.SetActive(false);
        backButton.SetActive(false);
    }

    public void openSavesList() {
        mainList.SetActive(false);
        savesList.SetActive(true);
        backButton.SetActive(true);
    }

    public void openMainList() {
        mainList.SetActive(true);
        savesList.SetActive(false);
        backButton.SetActive(false);
    }
}

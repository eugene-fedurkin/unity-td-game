using UnityEngine;

public class MainMenuListController : MonoBehaviour {
    [SerializeField] private GameObject mainList;
    [SerializeField] private GameObject savesList;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject loadButton;

    private void Start() {
        mainList.SetActive(true);
        savesList.SetActive(false);
        backButton.SetActive(false);
        loadButton.SetActive(false);
    }

    public void openSavesList() {
        mainList.SetActive(false);
        savesList.SetActive(true);
        backButton.SetActive(true);
        loadButton.SetActive(false);
    }

    public void openMainList() {
        mainList.SetActive(true);
        savesList.SetActive(false);
        backButton.SetActive(false);
        loadButton.SetActive(false);
    }
}

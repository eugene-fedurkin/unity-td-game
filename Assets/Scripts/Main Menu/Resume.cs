using System;
using TMPro;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField] GameDataManager gameDataManager;
    [SerializeField] TextMeshProUGUI dateTimeText;
    [SerializeField] String format = "MM-dd";


    void Start()
    {
        DateTime? dateTime = gameDataManager.getLastSavedSessionDate();

        if (dateTime == null) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            dateTimeText.text = dateTime?.ToString("ddd h:m");
        }
    }
}

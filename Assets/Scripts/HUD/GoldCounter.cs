using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour {
    TMP_Text text;
    int counter = 0;

    void Start()
    {
        text = gameObject.GetComponent<TMP_Text>();
        updateCounter();
        GlobalEventManager.onUnitKilled.AddListener((GameObject unitObj) =>
        {
            UnitBehaviour unit = unitObj.GetComponent<UnitBehaviour>();
            counter += unit.gold;
            updateCounter();
        });
    }

    void updateCounter() {
        text.text = counter.ToString();
    }
}

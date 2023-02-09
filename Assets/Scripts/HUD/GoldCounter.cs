using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour {
    [SerializeField] World world;

    TMP_Text text;
    int counter = 0;

    void Start() {
        text = gameObject.GetComponent<TMP_Text>();
        updateCounter();
        GlobalEventManager.onUnitKilled.AddListener((GameObject unitObj) => {
            UnitBehaviour unit = unitObj.GetComponent<UnitBehaviour>();
            counter += unit.gold;
            GameDataManager.instance.patchSession(session => session.gold = counter);
            updateCounter();
        });
    }

    public void initDeps() {
        counter = GameDataManager.instance.getLastSession()?.gold ?? 0;
    }

    void updateCounter() {
        text.text = counter.ToString();
    }
}

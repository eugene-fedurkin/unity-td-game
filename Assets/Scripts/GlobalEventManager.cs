using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour {
    static public UnityEvent<GameObject> onUnitCreate = new UnityEvent<GameObject>();
    static public UnityEvent<GameObject> onUnitDeath = new UnityEvent<GameObject>();

    static public UnityEvent onEndWave = new UnityEvent();
    
    static public UnityEvent onBaseDeath = new UnityEvent();


    static public void unitCreate(GameObject unit) {
        onUnitCreate.Invoke(unit);
    }

    static public void unitDeath(GameObject unit) {
        onUnitDeath.Invoke(unit);
    }

    static public void endWave() {
        onEndWave.Invoke();
    }

    static public void baseDeath() {
        onBaseDeath.Invoke();
    }
}

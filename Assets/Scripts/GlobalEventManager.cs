using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour {
    static public UnityEvent<GameObject> onUnitCreate = new UnityEvent<GameObject>();
    static public UnityEvent<GameObject> onUnitDeath = new UnityEvent<GameObject>();
    static public UnityEvent<GameObject> onUnitKilled = new UnityEvent<GameObject>();
    static public UnityEvent onEndWave = new UnityEvent();
    static public UnityEvent onBaseDeath = new UnityEvent();
    static public UnityEvent onRefreshLevel = new UnityEvent();
    static public UnityEvent<String, GameSession> onLoadScene = new UnityEvent<String, GameSession>();
    static public UnityEvent onAllSpawnsFinished = new UnityEvent();

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    static public void unitCreate(GameObject unit) {
        onUnitCreate.Invoke(unit);
    }

    static public void unitDeath(GameObject unit) {
        onUnitDeath.Invoke(unit);
    }

    static public void unitKilled(GameObject unit) {
        onUnitKilled.Invoke(unit);
    }

    static public void endWave() {
        onEndWave.Invoke();
    }

    static public void baseDeath() {
        onBaseDeath.Invoke();
    }

    static public void refreshLevel() {
        onRefreshLevel.Invoke();
    }

    static public void loadScene(String sceneName, GameSession session) {
        onLoadScene.Invoke(sceneName, session);
    }

    static public void allSpawnsFinished() {
        onAllSpawnsFinished.Invoke();
    }
}

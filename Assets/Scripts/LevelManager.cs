using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour {
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] GameObject startWaveButton;

    public static UnityEvent onWaveEnd = new UnityEvent();

    private void Awake()
    {
        GlobalEventManager.onEndWave.AddListener(() => {
            Debug.Log("onEndWave");
            startWaveButton.SetActive(true);
        });
    }

    public static void waveEnd() {
        onWaveEnd.Invoke();
    }

    void Start() {
        spawnManager.initiateSpawns();
    }

    public void startWave() {
        bool isActivated = spawnManager.triggerSpawn();

        if (isActivated) {
            startWaveButton.SetActive(false);
        }
    }
}

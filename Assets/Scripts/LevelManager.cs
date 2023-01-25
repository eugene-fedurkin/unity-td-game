using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour {
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] BaseManager baseManager;
    [SerializeField] GameObject startWaveButton;
    [SerializeField] GameObject resetButton;

    private void Awake()
    {
        GlobalEventManager.onEndWave.AddListener(() => startWaveButton.SetActive(true));
        GlobalEventManager.onBaseDeath.AddListener(() => {
            resetButton.SetActive(true);
            Debug.Log("123");
        } );
    }

    void Start() {
        spawnManager.initiateSpawns();
        baseManager.initiateBase();
    }

    public void startWave() {
        bool isActivated = spawnManager.triggerSpawn();

        if (isActivated) {
            startWaveButton.SetActive(false);
        }
    }
}

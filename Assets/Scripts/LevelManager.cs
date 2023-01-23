using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour {
    [SerializeField] SpawnManager spawnManager;

    void Start() {
        spawnManager.initiateSpawns();
        spawnManager.activateSpawn(0);
    }

    void Update() {
        
    }
}

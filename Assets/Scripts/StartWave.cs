using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour {
    [SerializeField] LevelManager levelManager;

    public void startWave() {
        levelManager.startWave();
    }
}

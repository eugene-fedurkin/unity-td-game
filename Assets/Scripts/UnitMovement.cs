using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    int health;
    int speed;
    int power;
    int progress;
    int gold;
    
    [SerializeField] PathManager pathManager;

    Vector3 positionTo;

    void Start() {
        speed = 30;
        updatePositionLink(transform.position);
    }

    void Update() {
        if (transform.position == positionTo) {
            updatePositionLink(positionTo);
        }

        transform.position = Vector3.MoveTowards(transform.position, positionTo, speed / 10 * Time.deltaTime);
    }

    void updatePositionLink(Vector3 position) {
        Vector3 pos = pathManager.getNextCoordinate(position);
        positionTo = new Vector3(pos.x, transform.position.y, pos.z);
    }
}

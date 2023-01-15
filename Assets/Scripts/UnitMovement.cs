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

    Coordinates positionTo;

    void Start() {
        speed = 30;
        positionTo = pathManager.getNextCoordinate(transform.position);
    }

    void Update() {
        if (transform.position.x == positionTo.x && transform.position.z == positionTo.z) {
            positionTo = pathManager.getNextCoordinate(transform.position);
            if (positionTo == null) {
                Destroy(gameObject);
            }
        }

        if (positionTo != null) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(positionTo.x, transform.position.y, positionTo.z), speed / 10 * Time.deltaTime);
        }
    }
}

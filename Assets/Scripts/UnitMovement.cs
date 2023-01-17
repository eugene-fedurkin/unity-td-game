using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour {
    [SerializeField] int health;
    int speed;
    int power;
    int gold;

    public float progress;

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
            } else {
                rotateUnit();
            }
        }

        if (positionTo != null) {
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, new Vector3(positionTo.x, transform.position.y, positionTo.z), speed / 10 * Time.deltaTime);
            progress += Vector3.Distance(nextPosition, transform.position);
            transform.position = nextPosition;
        }
    }

    public void getDamage(int damage) {
        Debug.Log("GET DAMAGE");
        health -= damage;
        if (health < 0)
        {
           Destroy(gameObject);
        }
    }

    void rotateUnit() {
        transform.rotation = Quaternion.LookRotation(new Vector3(positionTo.x, transform.position.y, positionTo.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z));

        /*
        if (positionTo.x < transform.position.x) {
            transform.eulerAngles = new Vector3(0, -90, 0);
        } else if (positionTo.x > transform.position.x) { 
            transform.eulerAngles = new Vector3(0, 90, 0);
        } else if (positionTo.z > transform.position.z) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (positionTo.z < transform.position.z) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {
    [SerializeField] int health;
    int speed;
    public int power;
    int gold;
    PathManager pathManager;

    public float progress;

    Coordinates positionTo;

    

    void Start() {
        pathManager = FindObjectOfType<PathManager>();
        speed = 30;
        positionTo = pathManager.getNextCoordinate(transform.position);
        GlobalEventManager.unitCreate(gameObject);
    }

    void Update() {
        if (transform.position.x == positionTo.x && transform.position.z == positionTo.z) {
            positionTo = pathManager.getNextCoordinate(transform.position);
            if (positionTo == null) {
                unitDeath();
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
        if (health < 0) {
            unitDeath();
        }

        /*hightlight*/
        float value = Mathf.Ceil(255 * health / 10);
        Renderer cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", new Color(value, value, value));
    }

    void unitDeath()
    {
        GlobalEventManager.unitDeath(gameObject);
        Destroy(gameObject);
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

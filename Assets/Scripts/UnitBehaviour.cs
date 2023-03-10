using UnityEngine;

public class UnitBehaviour : MonoBehaviour {
    public int health;
    public int speed;
    public int power;
    public int gold;
    PathManager pathManager;

    public float progress;

    Coordinates positionTo;

    void Start() {
        pathManager = FindObjectOfType<PathManager>();
        positionTo = pathManager.getNextCoordinate(transform.position);
        rotateUnit();
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
        health -= damage;
        if (health < 0) {
            GlobalEventManager.unitKilled(gameObject);
            unitDeath();
        }

        /*hightlight*/
        float value = Mathf.Ceil(255 * health / 10);
        Renderer cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", new Color(value, value, value));
    }

    void unitDeath() {
        GlobalEventManager.unitDeath(gameObject);
        Destroy(gameObject);
    }

    void rotateUnit() {
        transform.rotation = Quaternion.LookRotation(new Vector3(positionTo.x, transform.position.y, positionTo.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
}

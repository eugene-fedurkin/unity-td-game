using UnityEngine;

public class Bullet : MonoBehaviour {
    Transform target;
    float speed;
    int damage;

    void Update() {
        if (target == null) {
            Debug.Log("DESTROY");
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {
            hitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void init(Transform targetProp, float speedProp, int damageProp) {
        target = targetProp;
        speed = speedProp;
        damage = damageProp;
    }

    void hitTarget() {
        UnitBehaviour e = target.GetComponent<UnitBehaviour>();
        e.getDamage(damage);
        Destroy(gameObject);
    }
}

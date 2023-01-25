using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    [SerializeField] int health;

    private void OnTriggerEnter(Collider other)
    {
        UnitBehaviour unit = other.GetComponent<UnitBehaviour>();

        if (unit != null) {
            health -= unit.power;

            if (health <= 0) {
                GlobalEventManager.baseDeath();
                Destroy(gameObject);
            }
        }
    }
}

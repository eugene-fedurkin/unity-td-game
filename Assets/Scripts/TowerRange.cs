using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour {
    [SerializeField] int interval;
    [SerializeField] int damage;

    UnitBehaviour targetToDamage;


    void Start() {
        StartCoroutine(startAttackCO());
    }

    IEnumerator startAttackCO() {
        yield return new WaitForSeconds(interval);
        startAttack();
        StartCoroutine(startAttackCO());
    }

    void startAttack() {
        if (!targetToDamage) {
            targetToDamage = getLargestElementUsingFor();
        }

        if (targetToDamage) {
            targetToDamage.getDamage(damage);
        }
    }

    UnitBehaviour getLargestElementUsingFor() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x);
        UnitBehaviour maxElement = null;

        for (int index = 1; index < colliders.Length; index++) {
            UnitBehaviour unitMovement = colliders[index].GetComponent<UnitBehaviour>();
            if (unitMovement && (!maxElement || unitMovement.progress > maxElement.progress)) {
                maxElement = unitMovement;
            }
            
        }

        return maxElement;
    }
}

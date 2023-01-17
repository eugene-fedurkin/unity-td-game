using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour {
    List<UnitMovement> unitsInRange;
    [SerializeField] int interval;
    [SerializeField] int damage;

    UnitMovement targetToDamage;


    void Start() {
        unitsInRange = new List<UnitMovement>();
    }

    void OnTriggerEnter(Collider other) {
        UnitMovement unit = other.GetComponent<UnitMovement>();
        if (unit) {
            unitsInRange.Add(unit);
        }

        StartCoroutine(startAttackCO());
    }

    void OnTriggerExit(Collider other) {
        UnitMovement unit = other.GetComponent<UnitMovement>();
        if (unit) {
            unitsInRange.Remove(unit);
        }
    }

    IEnumerator startAttackCO() {
        yield return new WaitForSeconds(interval);
        if (startAttack()) {
            StartCoroutine(startAttackCO());
        }
    }

    bool startAttack() {
        if (!targetToDamage) {
            targetToDamage = getLargestElementUsingFor(unitsInRange);
        }

        if (targetToDamage) {
            targetToDamage.getDamage(damage);

            return true;
        }

        return false;
    }

    UnitMovement getLargestElementUsingFor(List<UnitMovement> sourceArray)
    {
        UnitMovement maxElement = sourceArray[0];
        for (int index = 1; index < sourceArray.Count; index++)
        {
            if (sourceArray[index].progress > maxElement.progress) {
                maxElement = sourceArray[index];

            }
        }

        return maxElement;
    }
}

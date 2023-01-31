using System.Collections;
using UnityEngine;

public class TowerRange : MonoBehaviour {
    public int interval;
    public int damage;
    public float bulletSpeed;
    public GameObject bulletPrefab;

    UnitBehaviour targetToDamage;

    public void startTrackUnit() {
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
            createBullet(targetToDamage);
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

    void createBullet(UnitBehaviour targetToDamage) {
        GameObject bulletGameObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.init(targetToDamage.transform, bulletSpeed, damage);
        bulletGameObject.transform.parent = gameObject.transform;
    }
}

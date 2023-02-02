using System.Collections;
using UnityEngine;

public class TowerRange : MonoBehaviour {
    int interval;
    int damage;
    float bulletSpeed;
    GameObject bulletPrefab;

    UnitBehaviour targetToDamage;

    public void startTrackUnit() {
        StartCoroutine(startAttackCO());
    }

    public void setConfiguration(int intervalProp, int damageProp, float bulletSpeedProp, GameObject bulletPrefabProp) {
        interval = intervalProp;
        damage = damageProp;
        bulletSpeed = bulletSpeedProp;
        bulletPrefab = bulletPrefabProp;
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
        Debug.Log(gameObject.name);
        GameObject bulletGameObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
        bullet.init(targetToDamage.transform, bulletSpeed, damage);
        bulletGameObject.transform.parent = gameObject.transform;
    }
}

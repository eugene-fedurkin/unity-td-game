using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour {
    [SerializeField] World world;
    [SerializeField] GameObject basePrefab;
    [SerializeField] PathManager pathManager;

    private GameObject baseObject;
    int baseInexPosition;

    public void initDeps() {
        baseInexPosition = world.getLevel(GameDataManager.instance.getLevel()).basePosition;
    }

    public void refreshBase()  {
        if (baseObject != null) {
            Destroy(baseObject.gameObject);
        }

        baseObject = Instantiate(basePrefab, getPosition(baseInexPosition), Quaternion.identity);
        baseObject.transform.parent = gameObject.transform;
    }

    Vector3 getPosition(int idx) {
        Vector3 vector = pathManager.getEndtByIndex(idx);

        return new Vector3(vector.x, 1, vector.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] GameObject basePrefab;
    [SerializeField] Vector3 basePosition;


    public void initiateBase()  {
        Instantiate(basePrefab, basePosition, Quaternion.identity);
    }
}

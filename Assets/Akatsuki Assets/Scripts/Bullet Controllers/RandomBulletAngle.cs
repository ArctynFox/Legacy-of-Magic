using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBulletAngle : MonoBehaviour
{
    public BulletInstantiator bulletSpawnScript;
    public float startDegree = 30f;
    public float endDegree = 150f;

    // Update is called once per frame
    void FixedUpdate()
    {
        bulletSpawnScript.arcCenter = Random.Range(startDegree, endDegree);
    }
}

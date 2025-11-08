using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOrbitAngle : MonoBehaviour
{
    public BulletInstantiator spawnScript;
    public float leftBound = -30f;
    public float rightBound = 30f;

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnScript.orbitAngle = Random.Range(leftBound, rightBound);
    }
}

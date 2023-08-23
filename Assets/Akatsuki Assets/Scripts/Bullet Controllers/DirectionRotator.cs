using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionRotator : MonoBehaviour
{
    public float degreesPerSecond = 15f;
    public BulletInstantiator[] instantiators;
    public bool angleIncreasing = false;
    public float deltaAngle = 15f;
    float degreesPerFrame;
    // Start is called before the first frame update
    void Start()
    {
        degreesPerFrame = degreesPerSecond / 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (angleIncreasing)
        {
            degreesPerFrame += deltaAngle * Time.fixedDeltaTime;
        }
        foreach(BulletInstantiator current in instantiators)
        {
            current.arcCenter += degreesPerFrame;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotationOnOtherSpawner : MonoBehaviour
{
    public BulletInstantiator from;
    public BulletInstantiator to;
    public float secondsUntilStart = 2;
    public float spawnsPerSecond = 1/(4.5f);
    int framesPerSpawn = 0;
    int framesSinceLastUpdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        framesSinceLastUpdate = (int)-Mathf.Floor(secondsUntilStart * 50);
        framesPerSpawn = (int)Mathf.Floor(50 / spawnsPerSecond);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(framesSinceLastUpdate == framesPerSpawn)
        {
            to.arcCenter = from.arcCenter;
            framesSinceLastUpdate = 0;
        }
        framesSinceLastUpdate++;
    }
}

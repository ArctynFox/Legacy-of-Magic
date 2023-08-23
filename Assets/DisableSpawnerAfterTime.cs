using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpawnerAfterTime : MonoBehaviour
{
    public BulletInstantiator spawner;
    public float secondsUntilStop = 1;
    int framesUntilStop;
    int framesUntilStart;
    int currentFrame = 0;
    int framesSinceStart = 0;
    // Start is called before the first frame update
    void Start()
    {
        framesUntilStart = (int)(spawner.secondsBeforeFirstSpawn * 50) + (int)Mathf.Floor(50 / spawner.spawnsPerSecond);
        framesUntilStop = (int)(secondsUntilStop * 50);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentFrame >= framesUntilStart)
        {
            framesSinceStart++;
        }
        if(framesSinceStart >= framesUntilStop)
        {
            spawner.enabled = false;
            enabled = false;
        }
        currentFrame++;
    }
}

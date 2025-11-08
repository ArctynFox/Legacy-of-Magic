using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLineRotationRange : MonoBehaviour
{
    public BulletInstantiator spawnScript;
    public float maxAngle = 90;
    public float cycleTime = 1f;
    float framesSinceDirectionChange = 0;
    float anglePerFrame;
    float startingAngle;
    // Start is called before the first frame update
    void Start()
    {
        startingAngle = spawnScript.arcCenter;
        anglePerFrame = maxAngle / (50 * cycleTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (framesSinceDirectionChange == 50 * cycleTime)
        {
            framesSinceDirectionChange = 0;
            anglePerFrame = anglePerFrame * -1;
        }
        spawnScript.arcCenter += anglePerFrame;
        framesSinceDirectionChange++;
    }
}

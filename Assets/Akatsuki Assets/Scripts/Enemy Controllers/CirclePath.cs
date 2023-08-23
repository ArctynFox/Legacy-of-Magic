using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour
{
    public MoveDanmaku moveScript;
    public float loopTime = 3f;
    public float startTime = 2f;
    float angle;
    float frameNumber = 0;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = moveScript.direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(frameNumber == startTime * 50)
        {
            angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - (7.2f / loopTime));
            direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            moveScript.direction = direction;
        }
        else if ((frameNumber > startTime * 50) && (frameNumber < (startTime + loopTime) * 50))
        {
            angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - (7.2f / loopTime));
            direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            moveScript.direction = direction;
        }
        frameNumber++;
    }
}

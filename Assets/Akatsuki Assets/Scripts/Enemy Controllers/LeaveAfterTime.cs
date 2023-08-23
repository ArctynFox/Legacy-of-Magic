using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveAfterTime : MonoBehaviour
{
    float framesSinceSpawn = 0;
    public float secondsUntilLeave = 10f;
    public float moveSpeed = 1f;
    float framesUntilLeave;
    // Start is called before the first frame update
    void Start()
    {
        framesUntilLeave = secondsUntilLeave * 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(framesSinceSpawn >= framesUntilLeave)
        {
            transform.position += -Vector3.up * moveSpeed * Time.fixedDeltaTime;
        }
        framesSinceSpawn++;
    }
}

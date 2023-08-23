using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnSpawnAndStop : MonoBehaviour
{
    public float stopXValue = 99;
    public float stopYValue = 99;
    public float moveSpeed = 1;
    bool hasStopped = false;
    public Vector3 moveDir = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        if(stopXValue >= 99)
        {
            moveDir.x = 0;
        }
        if(stopYValue >= 99)
        {
            moveDir.y = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveDir.x != 0 && !hasStopped)
        {
            if (moveDir.x < 0)
            {
                if (transform.position.x > stopXValue)
                {
                    transform.position += new Vector3(moveDir.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else hasStopped = true;
            }
            else
            {
                if (transform.position.x < stopXValue)
                {
                    transform.position += new Vector3(moveDir.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else hasStopped = true;
            }
        }
        if(moveDir.y != 0 && !hasStopped)
        {
            if (moveDir.y < 0)
            {
                if (transform.position.y > stopYValue)
                {
                    transform.position += new Vector3(0, moveDir.y * moveSpeed * Time.fixedDeltaTime, 0);
                }
                else hasStopped = true;
            }
            else
            {
                if (transform.position.y < stopYValue)
                {
                    transform.position += new Vector3(0, moveDir.y * moveSpeed * Time.fixedDeltaTime, 0);
                }
                else hasStopped = true;
            }
        }
    }
}

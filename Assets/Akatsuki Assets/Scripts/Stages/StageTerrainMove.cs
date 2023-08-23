using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTerrainMove : MonoBehaviour
{
    Parameters param;

    public Vector3 movement = new Vector3(-.5f, 0, 0);
    public Vector3 resetPosition = new Vector3(-120f, 0, 0);
    public float resetLocationTrigger = 0;

    void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!param.isBoss)
        {
            if (transform.position.x >= resetLocationTrigger)
            {
                transform.position = transform.position + resetPosition;
            }
            transform.position = transform.position - (movement * Time.fixedDeltaTime);
        } 
        else
        {
            if (transform.position.x < resetLocationTrigger)
            {
                transform.position = transform.position - (movement * 8 * Time.fixedDeltaTime);
            }
        }
    }
}

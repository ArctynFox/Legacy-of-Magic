using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageMove : MonoBehaviour
{
    Parameters param;

    public GameObject[] paths = new GameObject[3];

    public Vector3 movement = new Vector3(-.5f, 0, 0);
    bool fixLocation = false;

    void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    void FixedUpdate()
    {
        if (!param.isBoss)
        {
            if (transform.position.x >= -120)
            {
                transform.position = transform.position + new Vector3(-40f, 0, 0);
            }
            transform.position = transform.position - (movement * Time.fixedDeltaTime);
        }
        else
        {
            if (!fixLocation)
            {
                float basePos = 0;
                foreach(GameObject path in paths)
                {
                    if(path.transform.position.x - 1 < basePos)
                    {
                        basePos = path.transform.position.x - 40f;
                    }
                }
                transform.position = new Vector3(basePos, transform.position.y, transform.position.z);
                fixLocation = true;
            }
            if (transform.position.x < -40)
            {
                transform.position = transform.position - (movement * 8 * Time.fixedDeltaTime);
            }
        }
    }
}

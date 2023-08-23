using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraWhenBoss : MonoBehaviour
{
    Quaternion from;
    public Vector3 toAngle = new Vector3(90, -90, 0);
    Quaternion to;
    float timeCount = 0.0f;
    Parameters param;

    private void Awake()
    {
        from = transform.rotation;
        to = Quaternion.Euler(toAngle);
    }
    private void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (param.isBoss)
        {
            transform.rotation = Quaternion.Slerp(from, to, timeCount);
            timeCount += Time.fixedDeltaTime;
        }
    }
}

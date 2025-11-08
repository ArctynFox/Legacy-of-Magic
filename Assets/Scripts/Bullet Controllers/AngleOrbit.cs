using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleOrbit : MonoBehaviour
{

    public GameObject firedFrom;
    public float offsetAngle = 75f;
    public bool isOrbit = false;
    MoveDanmaku moveScript;
    public float angleZeroSpeed = .125f;
    public float stopTime = 99f;
    int frame = 0;


    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MoveDanmaku>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOrbit && offsetAngle != 0f)
        {
            if (firedFrom != null)
            {
                direction = (transform.position - firedFrom.transform.position).normalized;
            }
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (offsetAngle /*/ 50*/));
            moveScript.direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            if (stopTime < 22)
            {
                offsetAngle -= angleZeroSpeed * new Vector2(offsetAngle, 0).normalized.x;
                frame++;
                if (stopTime * 50 == frame)
                {
                    offsetAngle = 0f;
                    isOrbit = false;
                }
            }
        }
    }
}

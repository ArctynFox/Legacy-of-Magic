using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDanmaku : MonoBehaviour
{
    public float moveSpeed = 1;
    public Vector3 direction = new Vector3(0,0);
    
    private void Start()
    {
    }

    void FixedUpdate()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

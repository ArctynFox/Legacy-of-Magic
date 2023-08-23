using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGravity : MonoBehaviour
{
    public float timeMultiplier = 1f;
    public float endDegree = -90f;
    public MoveDanmaku movementScript;
    public float gravity = -9.81f;

    public Vector3 velocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.y += gravity * timeMultiplier * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}

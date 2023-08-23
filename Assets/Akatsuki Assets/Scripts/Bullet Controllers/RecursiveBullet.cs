using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBullet : MonoBehaviour
{
    public BulletGravity gravityScript;
    public MoveDanmaku moveScript;
    public BulletInstantiator spawnScript;

    Vector3 direction;
    //Vector3 previousDirection;

    private void Start()
    {
        //previousDirection = moveScript.direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = moveScript.direction + gravityScript.velocity * Time.deltaTime;
        spawnScript.arcCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
    }
}

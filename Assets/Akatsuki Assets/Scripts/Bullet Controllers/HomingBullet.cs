using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float timeToStop = 3f;//time it takes from when the bullet is shot to when it slows to a stop, which is then followed by the bullet changing direction and moving toward the player's location at that time

    MoveDanmaku moveScript;
    Vector3 deltaDirection;
    Vector3 directionTowardPlayer;
    GameObject player;
    bool hasHomed = false;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MoveDanmaku>();
        deltaDirection = moveScript.direction / (timeToStop * 50);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveScript.direction != new Vector3() && !hasHomed)
        {
            moveScript.direction -= deltaDirection;
        }
        else if(!hasHomed)
        {
            directionTowardPlayer = (player.transform.position - transform.position).normalized;
            moveScript.direction = directionTowardPlayer;
            hasHomed = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    public float moveSpeed = 1;
    public float moveDirection = 90f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Mathf.Cos((moveDirection / 180) * Mathf.PI), Mathf.Sin((moveDirection / 180) * Mathf.PI), 0) * moveSpeed * Time.fixedDeltaTime;
    }
}

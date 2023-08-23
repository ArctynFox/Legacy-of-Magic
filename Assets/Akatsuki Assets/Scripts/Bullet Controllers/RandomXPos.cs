using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomXPos : MonoBehaviour
{
    Vector3 randomPos;
    public float leftBound = -11.5f;
    public float rightBound = 11.5f;
    // Start is called before the first frame update
    void Start()
    {
        randomPos.y = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        randomPos.x = Random.Range(leftBound, rightBound);
        transform.position = randomPos;
    }
}

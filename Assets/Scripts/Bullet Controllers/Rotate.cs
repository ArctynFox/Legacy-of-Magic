using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationDegreesPerFrame = new Vector3(0, 0, 1f);

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationDegreesPerFrame);
    }
}

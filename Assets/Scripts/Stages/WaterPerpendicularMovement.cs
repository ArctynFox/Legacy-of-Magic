using UnityEngine;

public class WaterPerpendicularMovement : MonoBehaviour
{
    public Vector3 movement = new Vector3(0, 0, -.5f);
    public Vector3 resetPosition = new Vector3(0, 0, -20f);
    public float resetLocationTrigger = -80f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z >= resetLocationTrigger)
        {
            transform.position = transform.position + resetPosition;
        }
        transform.position = transform.position - (movement * Time.fixedDeltaTime);
        
    }
}


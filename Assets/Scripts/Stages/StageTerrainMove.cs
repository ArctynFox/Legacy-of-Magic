using UnityEngine;

public class StageTerrainMove : MonoBehaviour
{
    public Vector3 movement = new Vector3(-.5f, 0, 0);
    public Vector3 resetPosition = new Vector3(-120f, 0, 0);
    public float resetLocationTrigger = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Parameters.singleton.isBoss)
        {
            if (transform.position.x >= resetLocationTrigger)
            {
                transform.position = transform.position + resetPosition;
            }
            transform.position = transform.position - (movement * Time.fixedDeltaTime);
        } 
        else
        {
            if (transform.position.x < resetLocationTrigger)
            {
                transform.position = transform.position - (movement * 8 * Time.fixedDeltaTime);
            }
        }
    }
}

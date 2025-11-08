using UnityEngine;

public class RotateCameraWhenBoss : MonoBehaviour
{
    Quaternion from;
    public Vector3 toAngle = new Vector3(90, -90, 0);
    Quaternion to;
    float timeCount = 0.0f;

    private void Awake()
    {
        from = transform.rotation;
        to = Quaternion.Euler(toAngle);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Parameters.singleton.isBoss)
        {
            transform.rotation = Quaternion.Slerp(from, to, timeCount);
            timeCount += Time.fixedDeltaTime;
        }
    }
}

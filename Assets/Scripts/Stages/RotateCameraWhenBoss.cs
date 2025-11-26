using UnityEngine;

//ボスが現れたらカメラを指定方向に向ける

public class RotateCameraWhenBoss : MonoBehaviour
{
    public Vector3 toAngle = new Vector3(90, -90, 0);
    Quaternion from;
    Quaternion to;
    float timeCount = 0.0f;

    private void Awake()
    {
        from = transform.rotation;
        to = Quaternion.Euler(toAngle);
    }
    
    void FixedUpdate()
    {
        if (Parameters.singleton.isBoss && timeCount < 1f)
        {
            transform.rotation = Quaternion.Slerp(from, to, timeCount);
            timeCount += Time.fixedDeltaTime;
        }
    }
}

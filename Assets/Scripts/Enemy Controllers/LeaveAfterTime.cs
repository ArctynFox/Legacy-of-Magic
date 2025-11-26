using UnityEngine;

//指定秒数の後、画面を去る

public class LeaveAfterTime : MonoBehaviour
{
    //スポーンから去るまでの秒数
    public float secondsUntilLeave = 10f;

    //去る移動速度
    public float moveSpeed = 1f;

    //スポーンからのフレーム数
    float framesSinceSpawn = 0;
    //スポーンから去るまでのフレーム数
    float framesUntilLeave;

    void Start()
    {
        framesUntilLeave = secondsUntilLeave * (int)(1 / Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if(framesSinceSpawn >= framesUntilLeave)
        {
            transform.position += moveSpeed * Time.fixedDeltaTime * -Vector3.up;
        }
        framesSinceSpawn++;
    }
}

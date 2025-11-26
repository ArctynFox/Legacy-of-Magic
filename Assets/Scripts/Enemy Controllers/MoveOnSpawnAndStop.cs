using UnityEngine;

//スポーンされたら、指定座標まで移動して停止

public class MoveOnSpawnAndStop : MonoBehaviour
{
    //移動座標閾値
    public float stopXValue = 99;
    public float stopYValue = 99;

    //移動速度
    public float moveSpeed = 1;

    //移動方向（正規化ベクトル）
    public Vector3 moveDir = new Vector3(0f, 0f, 0f);

    //動きはもう停止かどうか
    bool hasStopped = false;
    
    void Start()
    {
        //条件によって何もしない
        if(stopXValue >= 99)
        {
            moveDir.x = 0;
        }
        if(stopYValue >= 99)
        {
            moveDir.y = 0;
        }
    }

    void FixedUpdate()
    {
        //指定座標閾値まで指定方向に動く
        if(moveDir.x != 0 && !hasStopped)
        {
            if (moveDir.x < 0)
            {
                if (transform.position.x > stopXValue)
                {
                    transform.position += new Vector3(moveDir.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else hasStopped = true;
            }
            else
            {
                if (transform.position.x < stopXValue)
                {
                    transform.position += new Vector3(moveDir.x * moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else hasStopped = true;
            }
        }
        if(moveDir.y != 0 && !hasStopped)
        {
            if (moveDir.y < 0)
            {
                if (transform.position.y > stopYValue)
                {
                    transform.position += new Vector3(0, moveDir.y * moveSpeed * Time.fixedDeltaTime, 0);
                }
                else hasStopped = true;
            }
            else
            {
                if (transform.position.y < stopYValue)
                {
                    transform.position += new Vector3(0, moveDir.y * moveSpeed * Time.fixedDeltaTime, 0);
                }
                else hasStopped = true;
            }
        }
    }
}

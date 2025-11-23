using UnityEngine;

public class StageTerrainMove : MonoBehaviour
{
    //移動速度
    [Tooltip("The speed at which the ground should move.")]
    public float movementSpeed = .5f;

    //ボスがいる速度倍数
    [Tooltip("Speed multiplier to be applied when boss exists.")]
    public int bossAppearMovementMultiplier = 8;

    //リセット距離
    [Tooltip("The distance to move the object to reset its position.")]
    public float resetXDistance = -120f;

    //リセットトリガーX座標閾値
    [Tooltip("The X coordinate threshold at which to trigger a position reset.")]
    public float resetLocationTrigger = 0;

    void FixedUpdate()
    {
        //ボスがいない限り、普通の速度で移動し、リセット座標に着いたら転移
        if (!Parameters.singleton.isBoss)
        {
            if (transform.position.x >= resetLocationTrigger)
            {
                transform.position = transform.position + new Vector3(resetXDistance, 0, 0);
            }
            transform.position = transform.position - (new Vector3(-movementSpeed, 0, 0) * Time.fixedDeltaTime);
        }
        //ボスがいたら、カメラの視野から外れ、停止
        else
        {
            if (transform.position.x < resetLocationTrigger)
            {
                transform.position = transform.position - (bossAppearMovementMultiplier * Time.fixedDeltaTime * new Vector3(-movementSpeed, 0, 0));
            }
        }
    }
}

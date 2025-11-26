using UnityEngine;

//波があるように見せるために水テクスチャをオーバーレイするため
//機能的にはStageTerrainMoveに似ている

public class WaterPerpendicularMovement : MonoBehaviour
{
    //移動速度
    [Tooltip("The speed per second at which the object should move.")]
    public float movementSpeed = .5f;

    //リセット距離
    [Tooltip("The distance to move the object to reset its position.")]
    public float resetZDistance = -20f;

    //リセットトリガーZ座標閾値
    [Tooltip("The Z coordinate threshold at which to trigger a position reset.")]
    public float resetLocationTrigger = -80f;

    void FixedUpdate()
    {
        //移動し、リセット座標に着いたら転移
        if (transform.position.z >= resetLocationTrigger)
        {
            transform.position = transform.position + new Vector3(0, 0, resetZDistance);
        }
        transform.position = transform.position - (new Vector3(0, 0, -movementSpeed) * Time.fixedDeltaTime);
        
    }
}


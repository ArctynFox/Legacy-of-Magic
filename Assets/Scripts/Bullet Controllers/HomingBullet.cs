using UnityEngine;

//弾に追加すると、指定秒数まで弾の動きを減速し停止してから、プレイヤーの位置を記録し、そちらに真っ直ぐ飛ぶ

//弾専用
//デコレーターパターン
[RequireComponent(typeof(MoveDanmaku))]
public class HomingBullet : MonoBehaviour
{
    //停止までの秒数
    public float timeToStop = 3f;//time it takes from when the bullet is shot to when it slows to a stop, which is then followed by the bullet changing direction and moving toward the player's location at that time

    //ベース弾移動スクリプト
    public MoveDanmaku moveScript;
    //フレーム当たりの速度差ベクトル
    Vector3 deltaDirection;
    //弾からプレイヤーまでの方向ベクトル
    Vector3 directionTowardPlayer;
    //プレイヤーの位置に回転したかどうか
    bool hasHomed = false;

    void Start()
    {
        deltaDirection = moveScript.direction / (timeToStop * (int)(1 / Time.fixedDeltaTime));
    }

    void FixedUpdate()
    {
        //もう回転したなら何もしない
        if (hasHomed)
        {
            return;
        }

        //移動はまだ停止していないことを確認
        if(moveScript.direction != new Vector3())
        {
            //移動を減速
            moveScript.direction -= deltaDirection;
        }
        else
        {
            //プレイヤーの位置に回転
            directionTowardPlayer = (PlayerController.singleton.transform.position - transform.position).normalized;
            moveScript.direction = directionTowardPlayer;
            hasHomed = true;
        }
    }
}

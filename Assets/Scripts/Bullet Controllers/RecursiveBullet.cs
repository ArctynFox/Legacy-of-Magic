using UnityEngine;

//重力にかかわった弾に追加すると、弾スポナーを利用して繰り返し二つに分裂

//弾専用
//デコレーターパターン
[RequireComponent(typeof(MoveDanmaku))]
[RequireComponent(typeof(BulletSpawner))]
[RequireComponent(typeof(BulletGravity))]
public class RecursiveBullet : MonoBehaviour
{
    public BulletGravity gravityScript;
    public MoveDanmaku moveScript;
    public BulletSpawner spawnScript;

    //現在移動している方向
    Vector3 direction;

    void FixedUpdate()
    {
        direction = moveScript.direction + gravityScript.velocity * Time.deltaTime;
        spawnScript.arcCenter = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
    }
}

using UnityEngine;

//弾スポナーに追加すると、発射角度を指定範囲内でランダム化

//弾スポナー専用
//デコレーターパターン
[RequireComponent(typeof(BulletSpawner))]
public class RandomBulletAngle : MonoBehaviour
{
    public BulletSpawner bulletSpawnScript;
    //最低角度
    public float startDegree = 30f;
    //最高角度
    public float endDegree = 150f;

    void FixedUpdate()
    {
        bulletSpawnScript.arcCenter = Random.Range(startDegree, endDegree);
    }
}

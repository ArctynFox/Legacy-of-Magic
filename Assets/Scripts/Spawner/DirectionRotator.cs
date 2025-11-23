using UnityEngine;

//弾スポナーに追加すると、スポナーが指定角度を一秒あたり指定角度で回転（デルタも指定されている場合はその値で）

//弾スポナー専用
//デコレーターパターン
[RequireComponent(typeof(BulletSpawner))]
public class DirectionRotator : MonoBehaviour
{
    //一秒あたりの角度差
    public float degreesPerSecond = 15f;
    //対象スポナー
    public BulletSpawner[] instantiators;
    //角度は時間が立つと変化するかどうか
    public bool angleIncreasing = false;
    //一秒ごとの角度変更
    public float deltaAngle = 15f;
    //フレーム当たりの角度差
    float degreesPerFrame;
    
    void Start()
    {
        degreesPerFrame = degreesPerSecond / (int)(1 / Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if (angleIncreasing)
        {
            degreesPerFrame += deltaAngle * Time.fixedDeltaTime;
        }
        foreach(BulletSpawner current in instantiators)
        {
            //発射方向を開店
            current.arcCenter += degreesPerFrame;
        }
    }
}

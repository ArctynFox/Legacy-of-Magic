using UnityEngine;

//弾スポナーに追加すると、発射方向を指定された最大角度で前後に回転させる

//スポナー専用
[RequireComponent(typeof(BulletInstantiator))]
public class BulletLineRotationRange : MonoBehaviour
{
    public BulletInstantiator spawnScript;
    //最大角度
    public float maxAngle = 90;
    //回転方向を変更するタイミング
    public float cycleTime = 1f;
    //最近変更からのフレーム数
    float framesSinceDirectionChange = 0;
    //フレームごとの回転角度差
    float anglePerFrame;
    
    void Start()
    {
        //回転角度差を計算
        anglePerFrame = maxAngle / ((int)(1 / Time.fixedDeltaTime) * cycleTime);
    }

    void FixedUpdate()
    {
        if (framesSinceDirectionChange == (int)(1 / Time.fixedDeltaTime) * cycleTime)
        {
            framesSinceDirectionChange = 0;
            //回転方向を変化
            anglePerFrame = anglePerFrame * -1;
        }

        //発射角度を変更
        spawnScript.arcCenter += anglePerFrame;
        framesSinceDirectionChange++;
    }
}

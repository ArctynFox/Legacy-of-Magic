using UnityEngine;

//弾スポナーに追加すると、secondsBetweenAngleUpdateの指定秒数ごとにchildInstantiatorの発射角度をmasterInstantiatorのに設定

//スポナー専用
[RequireComponent(typeof(BulletSpawner))]
public class BaseRotationOnOtherSpawner : MonoBehaviour
{
    public BulletSpawner masterInstantiator;
    public BulletSpawner childInstantiator;
    public float secondsBetweenAngleUpdate = 2f;
    //発射当たりのフレーム数
    int framesPerSpawn;
    //最近発生からのフレーム数
    int framesSinceLastUpdate;
    
    void Start()
    {
        framesPerSpawn = (int)Mathf.Floor((int)(1 / Time.fixedDeltaTime) / masterInstantiator.spawnsPerSecond);
        //初発生前の初期フレーム数
        framesSinceLastUpdate = (int)-Mathf.Floor(secondsBetweenAngleUpdate * (int)(1 / Time.fixedDeltaTime));
    }

    void FixedUpdate()
    {
        //弾スポナーに追加すると、secondsBetweenAngleUpdateの指定秒数ごとにchildInstantiatorの発射角度をmasterInstantiatorのに設定
        if (framesSinceLastUpdate == framesPerSpawn)
        {
            childInstantiator.arcCenter = masterInstantiator.arcCenter;
            framesSinceLastUpdate = 0;
        }
        framesSinceLastUpdate++;
    }
}

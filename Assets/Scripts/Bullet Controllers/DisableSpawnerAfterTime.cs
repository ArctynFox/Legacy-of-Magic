using UnityEngine;

//弾スポナーに追加すると、初めての発射から指定秒数の後、発射を停止

[RequireComponent(typeof(BulletSpawner))]
public class DisableSpawnerAfterTime : MonoBehaviour
{
    public BulletSpawner spawner;
    //初発射から停止の秒数
    public float secondsUntilStop = 1f;
    //ロードから初発射のフレーム数
    int framesUntilStart;
    //初発射から停止のフレーム数
    int framesUntilStop;
    //フレームを数える変数
    int currentFrame = 0;
    //初発射からのフレーム数
    int framesSinceStart = 0;

    void Start()
    {
        framesUntilStart = (int)(spawner.secondsBeforeFirstSpawn * (int)(1 / Time.fixedDeltaTime)) + (int)Mathf.Floor((int)(1 / Time.fixedDeltaTime) / spawner.spawnsPerSecond);
        framesUntilStop = (int)(secondsUntilStop * (int)(1 / Time.fixedDeltaTime));
    }

    void FixedUpdate()
    {
        if(currentFrame >= framesUntilStart)
        {
            framesSinceStart++;
        }
        if(framesSinceStart >= framesUntilStop)
        {
            //発射を停止
            spawner.enabled = false;
            enabled = false;
        }
        currentFrame++;
    }
}

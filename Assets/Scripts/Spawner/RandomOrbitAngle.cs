using UnityEngine;

//弾スポナーに追加すると、発射する弾の軌道角度をランダム化

//弾スポナー専用
//デコレーターパターン
[RequireComponent(typeof(BulletSpawner))]
public class RandomOrbitAngle : MonoBehaviour
{
    public BulletSpawner spawnScript;
    //反時計回り最大限
    public float leftBound = -30f;
    //時計回り最大限
    public float rightBound = 30f;

    void FixedUpdate()
    {
        spawnScript.orbitAngle = Random.Range(leftBound, rightBound);
    }
}

using UnityEngine;

//弾スポナーに追加すると、X座標をランダム化

//弾スポナー専用
public class RandomXPos : MonoBehaviour
{
    Vector3 randomPos;
    public float leftBound = -11.5f;
    public float rightBound = 11.5f;
    
    void Start()
    {
        //Y座標の最初の位置を記録
        randomPos.y = transform.position.y;
    }

    void FixedUpdate()
    {
        randomPos.x = Random.Range(leftBound, rightBound);
        transform.position = randomPos;
    }
}

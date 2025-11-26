using UnityEngine;

//弾に追加すると、弾は指定速度で指定角度の方向に移動

//弾専用
//デコレーターパターンのベース
public class MoveDanmaku : MonoBehaviour
{
    //移動速度
    public float moveSpeed = 1;
    //移動方向ベクトル
    public Vector3 direction = new Vector3(0,0);
    
    void FixedUpdate()
    {
        //移動速度で移動方向に移動w
        transform.position += direction * moveSpeed * Time.deltaTime;
        //弾のグラフィックを移動方向に向ける
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

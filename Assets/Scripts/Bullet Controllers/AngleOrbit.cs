using UnityEngine;

//弾にComponentとして追加すると、発射したGameObjectを中心に軌道運動をする

//Decoratorパターン
[RequireComponent(typeof(MoveDanmaku))]
public class AngleOrbit : MonoBehaviour
{
    //発射したGameObject
    public GameObject firedFrom;
    //発射したGameObjectからの射線と弾の弾道の角度差
    public float offsetAngle = 75f;
    //弾は発射したGameObjectの周りを軌道するかどうか
    public bool isOrbit = false;
    //弾の一般的運動機能スクリプト
    MoveDanmaku moveScript;
    //角度の変化がゼロに近づく係数
    public float angleZeroFactor = .125f;
    //軌道を停止するまでの秒数
    public float stopTime = 99f;
    //FixedUpdate起動回数
    int frame = 0;

    //発射したGameObjectからの方向ベクトル
    Vector3 direction;
    
    void Start()
    {
        moveScript = GetComponent<MoveDanmaku>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOrbit && offsetAngle != 0f)
        {
            if (firedFrom != null)
            {
                //向いベクトルを正規化
                direction = (transform.position - firedFrom.transform.position).normalized;
            }
            //新しい移動方向を計算し、設定
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (offsetAngle));
            moveScript.direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            //TODO: would be good to revise this part so that negative or zero stopTime causes the bullet to not stop orbiting
            if (stopTime < 22)
            {
                //angleZeroFactorを使ってoffsetAngleを直す
                offsetAngle -= angleZeroFactor * new Vector2(offsetAngle, 0).normalized.x;
                
                //frameをインクリメントし、stopTimeが経過した場合、軌道を停止
                frame++;
                if (stopTime * (int)(1f / Time.fixedDeltaTime) <= frame)
                {
                    offsetAngle = 0f;
                    isOrbit = false;
                }
            }
        }
    }
}

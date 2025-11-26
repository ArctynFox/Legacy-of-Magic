using UnityEngine;

//ボス戦地面のGameObjectに追加すると、ボスが表れたら、距離フォグの範囲に入る

public class BossStageMove : MonoBehaviour
{
    //普通地面の参照
    [Tooltip("Object references to the stage's normal ground objects.")]
    public GameObject[] paths = new GameObject[3];

    //ボス戦地面はもう最も遠い普通地面の一番端に転移されたかどうか
    bool fixLocation = false;

    //地面GameObjectの設定された長さ
    const float PATH_LENGTH = 40f;

    void FixedUpdate()
    {
        //ボスがまだ現れていないなら何もしない
        if(!Parameters.singleton.isBoss)
        {
            return;
        }

        //まだ転移しなかったら転移
        if (!fixLocation)
        {
            //最も遠い普通地面の一番端に転移
            float basePos = 0;
            foreach(GameObject path in paths)
            {
                if(path.transform.position.x - 1 < basePos)
                {
                    basePos = path.transform.position.x - PATH_LENGTH;
                }
            }
            transform.position = new Vector3(basePos, transform.position.y, transform.position.z);
            fixLocation = true;
        }

        //カメラに映るまで指定移動速度で近づく
        if (transform.position.x < -PATH_LENGTH)
        {
            StageTerrainMove sTM = paths[0].GetComponent<StageTerrainMove>();
            transform.position = transform.position - (Time.fixedDeltaTime * new Vector3(-sTM.movementSpeed * sTM.bossAppearMovementMultiplier, 0, 0));
        }
    }
}

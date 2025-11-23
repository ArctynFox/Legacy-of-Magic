using UnityEngine;

//コライダーのあるGameObjectに追加すると、ぶつかった弾を削除し、指定アニメーションを再生

public class BulletDestroyer : MonoBehaviour
{
    //弾削除アニメーションプレハブ
    public GameObject destroyEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("PlayerBullet"))
        {
            Instantiate(destroyEffect, other.transform).transform.SetParent(null);
            Destroy(other.gameObject);
        }
    }
}

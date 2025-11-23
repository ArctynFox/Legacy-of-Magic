using UnityEngine;

//敵に追加すると、プレイヤーが発射した弾と衝突したら、敵のHPを減少

public class OnCollisionWithBullets : MonoBehaviour
{
    //普通の敵のHP
    public int health = 1;

    //敵を倒した際のスコアに加算される数値
    public int scoreWeight = 100;

    //敵が死んだらこのアニメーションプレハブをスポーン
    public GameObject deathParticles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health--;

            //敵が死んだら
            if(health <= 0)
            {
                Instantiate(deathParticles, transform).transform.SetParent(null);
                Parameters.singleton.score += scoreWeight;
                //自分を削除
                Destroy(gameObject);
            }

            //衝突した弾を削除
            Destroy(other.gameObject);
        }
    }
}

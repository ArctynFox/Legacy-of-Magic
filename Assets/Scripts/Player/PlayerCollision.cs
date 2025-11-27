using System.Collections;
using UnityEngine;

//プレイヤーがダメージを受けたら

public class PlayerCollision : MonoBehaviour
{
    //プレイヤー死アニメーションプレハブ
    [Tooltip("The prefab to spawn when the player takes damage.")]
    public GameObject deathExplosion;
    //衝突中かどうか
    [Tooltip("Determines whether the player is currently in a collision. Used so that only one hit can be taken at a time.")]
    public bool isColliding = false;
    //リスポーン位置
    [Tooltip("The coordinate position to respawn the player upon taking damage.")]
    public Vector3 respawnPosition = new Vector3(0f, -7f, 0f);
    //無敵秒数
    [Tooltip("Amount of time in seconds to be invulnerable.")]
    public float invulnerabilityTime = 3f;

    //コライダーのあるオブジェクトと衝突したら起動
    //this gets called every time the player's hurtbox collides with a bullet or enemy's hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        //もう衝突中だったら何もしない
        //skip if the player is already in a collision
        if (isColliding)
        {
            return;
        }
        isColliding = true;
        
        //衝突したオブジェクトのtagを確認
        //check the type of entity the collider hit was
        switch (other.gameObject.tag)
        {
            //弾だったら弾を削除し、敵のケースに進む
            //if hit by a bullet, destroy the bullet
            case "Bullet":
                Destroy(other);
                goto case "Enemy";
            //敵だったら
            //if hit by an enemy or a bullet, activate invulnerability
            case "Enemy":
                {
                    //プレイヤーにダメージを与える
                    Parameters.singleton.playerTookDamage();
                    //スペルカード数をリセット
                    Parameters.singleton.setCurrentBombs();

                    //敵だったら敵のHPを減少
                    if (other.gameObject.CompareTag("Enemy"))
                    {
                        other.GetComponent<OnCollisionWithBullets>().health--;
                    }
                    else if(other.gameObject.CompareTag("Boss"))
                    {
                        other.GetComponent<BossController>().health--;
                    }

                    //死アニメーションをスポーン
                    Instantiate(deathExplosion, transform).transform.parent = null;
                    //プレイヤーをリスポーン位置に転移
                    transform.position = respawnPosition;

                    //無敵クールダウン
                    StartCoroutine(WasHit());
                    break;
                }
            //ボスだったら敵として扱う
            case "Boss":
                goto case "Enemy";
            //何もしない
            default:
                isColliding = false;
                break;
        }
    }
    
    //指定秒数無敵タイマー
    //invulnerability timer
    IEnumerator WasHit()
    {
        yield return new WaitForSeconds(invulnerabilityTime);
        isColliding = false;
    }
}

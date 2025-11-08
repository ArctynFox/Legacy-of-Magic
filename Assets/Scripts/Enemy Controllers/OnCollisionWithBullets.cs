using UnityEngine;

public class OnCollisionWithBullets : MonoBehaviour
{
    public int health = 1;
    public int scoreWeight = 100;
    public GameObject deathParticles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            health--;
            Instantiate(deathParticles, transform).transform.SetParent(null);
            if(health < 1)
            {
                Parameters.singleton.score += scoreWeight;
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}

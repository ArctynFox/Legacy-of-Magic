using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OnCollisionWithBullets : MonoBehaviour
{
    Parameters param;
    public int health = 1;
    public int scoreWeight = 100;
    public GameObject deathParticles;

    private void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            health--;
            Instantiate(deathParticles, transform).transform.SetParent(null);
            if(health < 1)
            {
                param.score += scoreWeight;
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}

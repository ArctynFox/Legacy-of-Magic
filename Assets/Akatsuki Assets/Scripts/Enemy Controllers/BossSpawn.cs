using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject enemyEffect;
    public GameObject bulletEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other);
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(bulletEffect, other.transform).transform.SetParent(null);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("enemy is supposed to be destroyed by boss spawning");
            Instantiate(enemyEffect, other.transform).transform.SetParent(null);
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Parameters param;
    public GameObject deathExplosion;
    public bool isColliding = false;

    private void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    //this gets called every time the player's hurtbox collides with a bullet or enemy's hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        //skip if the player is already in a collision
        if (isColliding)
        {
            return;
        }
        isColliding = true;
        
        //check the type of entity the collider hit was
        switch (other.gameObject.tag)
        {
            //if hit by a bullet, destroy the bullet
            case "Bullet":
                Destroy(other);
                goto case "Enemy";
            //if hit by an enemy or a bullet, activate invulnerability
            case "Enemy":
                {
                    Vector3 resetPos = new Vector3(0, -7, 0);
                    param.playerTookDamage();
                    param.setBombs();
                    if (other.gameObject.tag == "Enemy")
                    {
                        other.GetComponent<OnCollisionWithBullets>().health--;
                    }
                    else if(other.gameObject.tag == "Boss")
                    {
                        other.GetComponent<BossCollision>().health--;
                    }
                    Instantiate(deathExplosion, transform).transform.parent = null;
                    transform.position = resetPos;
                    StartCoroutine("WasHit");
                    break;
                }
            case "Boss":
                goto case "Enemy";
            default:
                isColliding = false;
                break;
        }
    }
    
    //start invulnerability timer (3 seconds)
    IEnumerator WasHit()
    {
        yield return new WaitForSeconds(3f);
        isColliding = false;
    }
}

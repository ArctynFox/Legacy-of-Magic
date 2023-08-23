using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Parameters param;
    public GameObject deathExplosion;
    public bool isColliding = false;

    float framesSinceLastHit = 0;
    private void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    private void FixedUpdate()
    {
        if (framesSinceLastHit <= 50)
        {
            framesSinceLastHit++;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;
        if (framesSinceLastHit > 50)
        {
            switch (other.gameObject.tag)
            {
                case "Bullet":
                    Destroy(other);
                    goto case "Enemy";
                case "Enemy":
                    {
                        framesSinceLastHit = 0;
                        Vector3 resetPos = new Vector3(0, -7, 0);
                        param.lives--;
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
    }

    IEnumerator WasHit()
    {
        yield return new WaitForSeconds(3f);
        isColliding = false;
    }
}

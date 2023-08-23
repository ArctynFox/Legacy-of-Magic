using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;

    public GameObject emitter;
    public GameObject emitter2;
    public GameObject emitter3;
    public GameObject spell;
    public bool enemyCardActive = false;
    bool spellCooldown = false;
    int framesSinceEnemyCardStart = 0;
    Parameters param;

    Vector3 movement;

    void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
        param.playerCont = this;
    }

    // Update is called once per frame
    void Update() //input
    {
        if ((!enemyCardActive && Time.timeScale == 1) && GameObject.FindGameObjectsWithTag("Dialogue").Length < 1)
        {
            if (Input.GetKey("z"))
            {
                emitter.SetActive(true);
                emitter2.SetActive(true);
                emitter3.SetActive(true);
            }
            else
            {
                emitter.SetActive(false);
                emitter2.SetActive(false);
                emitter3.SetActive(false);
            }

            if (Input.GetKeyDown("x") && !spellCooldown && param.bombs >= 0)
            {
                Instantiate(spell, transform);
                param.bombs--;
                StartCoroutine(bombInvulnerability());
            }
        }
        else
        {
            emitter.SetActive(false);
            emitter2.SetActive(false);
            emitter3.SetActive(false);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if ((transform.position.x <= -11.5f && movement.x < 0) || (transform.position.x > 11.5f && movement.x > 0))
        {
            movement.x = 0;
        }
        if ((transform.position.y <= -9.05f && movement.y < 0) || (transform.position.y > 9.05f && movement.y > 0))
        {
            movement.y = 0;
        }
        
    }

    void FixedUpdate() //movement
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += movement * moveSpeed/2 * Time.fixedDeltaTime;
        } 
        else
        {
            transform.position += movement * moveSpeed * Time.fixedDeltaTime;
        }
        if (enemyCardActive)
        {
            if(framesSinceEnemyCardStart >= 100)
            {
                enemyCardActive = false;
                framesSinceEnemyCardStart = 0;
            }
            framesSinceEnemyCardStart++;
        }
    }

    IEnumerator bombInvulnerability()
    {
        GetComponent<PlayerCollision>().isColliding = true;
        spellCooldown = true;
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerCollision>().isColliding = false;
        spellCooldown = false;
    }
}

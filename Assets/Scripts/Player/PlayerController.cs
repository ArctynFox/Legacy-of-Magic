using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    // Start is called before the first frame update
    public float moveSpeed = 5f;

    public GameObject emitter;
    public GameObject emitter2;
    public GameObject emitter3;
    public GameObject spell;
    public bool firingDisabled = false;
    public bool isPostBoss = false;
    bool spellCooldown = false;
    int framesSinceEnemyCardStart = 0;

    Vector3 movement;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        } else singleton = this;
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    void UseBomb()
    {
        if(!spellCooldown && Parameters.singleton.bombs >= 0)
        {
            Instantiate(spell, transform);
            Parameters.singleton.bombs--;
            Parameters.singleton.updateBombDisplay();
            StartCoroutine(BombInvulnerability());
        }
    }

    void StartFire()
    {
        emitter.SetActive(true);
        emitter2.SetActive(true);
        emitter3.SetActive(true);
    }

    void StopFire()
    {
        emitter.SetActive(false);
        emitter2.SetActive(false);
        emitter3.SetActive(false);
    }

    // Update is called once per frame
    void Update() //input reading
    {
        if (!firingDisabled && Time.timeScale == 1 && GameObject.FindGameObjectsWithTag("Dialogue").Length < 1)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                StartFire();
            }
            else
            {
                StopFire();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                UseBomb();
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

        if (firingDisabled && !isPostBoss)
        {
            if(framesSinceEnemyCardStart >= 100)
            {
                firingDisabled = false;
                framesSinceEnemyCardStart = 0;
            }
            framesSinceEnemyCardStart++;
        }
    }

    IEnumerator BombInvulnerability()
    {
        GetComponent<PlayerCollision>().isColliding = true;
        spellCooldown = true;
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerCollision>().isColliding = false;
        spellCooldown = false;
    }
}

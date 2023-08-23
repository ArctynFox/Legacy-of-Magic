using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class BossCollision : MonoBehaviour
{
    Parameters param;
    public int healthBars = 3;
    public int phase = 0;
    public int maxHealth = 40;
    public int health;
    public GameObject bomb;
    public GameObject deathSprite;
    public GameObject[] emittersPrefabs;
    public GameObject[] emitters;
    public GameObject healthbarObject;
    public Slider healthbar;
    public int[] phaseHealths;
    public GameObject timer;
    public Text timerText;
    public float[] phaseTimeLimits;
    bool invulnerable;
    public GameObject bossDialoguePre;
    public GameObject bossDialoguePost;
    AdvanceDialogue preDialogue;
    bool isDead = false;
    public GameObject canvas;
    public GameObject enemyDeathAnim;
    public GameObject bulletDeathAnim;
    float currentTimeLimit;
    public bool dialogueOver = false;
    bool inCollision;

    void Start()
    {
        GameObject[] destroyList = GameObject.FindGameObjectsWithTag("Bullet");//when the boss spawns, all enemies and bullets need to die
        foreach(GameObject bullet in destroyList)
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
        destroyList = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject bullet in destroyList)
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
        destroyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in destroyList)
        {
            Instantiate(enemyDeathAnim, enemy.transform).transform.SetParent(null);
            Destroy(enemy);
        }
        healthBars = phaseHealths.Length;//failsafe in case I set healthBars incorrectly
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
        //healthbarObject = Instantiate(healthbarObject, GameObject.Find("Canvas").transform);
        //healthbar = GameObject.Find("Health Bar").GetComponent<Slider>();
        //param.bossDialoguePre = bossDialoguePre;
        health = phaseHealths[0];//the next few lines are all to set parameters and initiate pre-fight dialogue
        maxHealth = phaseHealths[0];
        bossDialoguePre = param.bossSpawned(bossDialoguePre);
        preDialogue = bossDialoguePre.GetComponentInChildren<AdvanceDialogue>();
        preDialogue.boss = gameObject;
        emitters = new GameObject[emittersPrefabs.Length];
        preDialogue.emitter0 = emittersPrefabs[0];
        preDialogue.healthbar = healthbarObject;
        preDialogue.timer = timer;
        preDialogue.firstTimeLimit = phaseTimeLimits[0];
        canvas = GameObject.Find("Canvas");
        currentTimeLimit = phaseTimeLimits[0];
    }

    void FixedUpdate()
    {
        if (dialogueOver)
        {
            currentTimeLimit -= .02f;
            timerText.text = currentTimeLimit.ToString();
        }
        if(currentTimeLimit <= 0 && dialogueOver)
        {
            if (phase < emittersPrefabs.Length - 1)
            {
                phase++;
                NextPhase();
            }
            else
            {
                if (!isDead)//another measure to make sure the boss doesn't get bugged
                {
                    param.isBossDead = true;
                    foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
                    {
                        Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);//gets rid of all player bullets, boss might bug otherwise
                        Destroy(bullet);
                    }
                    foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                    {
                        Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);//gets rid of all enemy bullets, it's unfair to still have to dodge during dialogue lmao
                        Destroy(bullet);
                    }
                    Instantiate(deathSprite, transform).transform.SetParent(null);//death explosion for boss
                    GameObject.Find("Player").GetComponent<PlayerController>().enemyCardActive = true;//prevents player from firing to make sure it doesn't initiate the post-fight dialogue multiple times
                    Instantiate(bossDialoguePost, canvas.transform);//instantiates the post-fight dialogue
                    emitters[emitters.Length - 1].SetActive(false);//sets the final bullet emitter to inactive
                    Destroy(emitters[emitters.Length - 1]);
                    dialogueOver = false;//another measure to make sure the player can't initiate dialogue a second time
                    Destroy(timer);//obvious
                }
            }

        }
        if(healthbar != null)
        {
            healthbar.value = health;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (inCollision)//one of the measures in place to make sure the boss doesn't get bugged
        {
            return;
        }
        inCollision = true;
        if (!invulnerable)//anther measure in place to make sure the boss doesn't break - this probably does the exact same as the previous if-statement but inverted
        {
            invulnerable = true;
            if (other.gameObject.tag == "PlayerBullet")//only takes damage/changes phase if hit by the player's bullets
            {
                health--;//decreases health by 1 per hit
                if (health < 1)
                {
                    phase++;//goes to next phase if health reaches 0
                    if (phase < healthBars)
                    {
                        param.score += 10000;//adds to score if phase change was initiated by one of the player's bullets
                        NextPhase();
                    }
                    else
                    {
                        if (!isDead)//another measure to make sure the boss doesn't get bugged
                        {
                            param.score += 50000;//adds to score if boss was beaten by player
                            param.isBossDead = true;
                            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
                            {
                                Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);//gets rid of all player bullets, boss might bug otherwise
                                Destroy(bullet);
                            }
                            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                            {
                                Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);//gets rid of all enemy bullets, it's unfair to still have to dodge during dialogue lmao
                                Destroy(bullet);
                            }
                            Instantiate(deathSprite, transform).transform.SetParent(null);//death explosion for boss
                            GameObject.Find("Player").GetComponent<PlayerController>().enemyCardActive = true;//prevents player from firing to make sure it doesn't initiate the post-fight dialogue multiple times
                            Instantiate(bossDialoguePost, canvas.transform);//instantiates the post-fight dialogue
                            emitters[emitters.Length - 1].SetActive(false);//sets the final bullet emitter to inactive
                            dialogueOver = false;//another measure to make sure the player can't initiate dialogue a second time
                            Destroy(timer);//obvious
                        }
                    }
                }
                Destroy(other.gameObject);//destroys the bullet that hit the boss
            }
            invulnerable = false;
        }
        inCollision = false;
    }

    void NextPhase()//changes properties and emitters for next boss phase
    {
        Instantiate(bomb, transform);//creates spellcard, gets rid of all bullets for 2 seconds
        emitters[phase - 1].SetActive(false);//turns off the previous emitter
        emitters[phase] = Instantiate(emittersPrefabs[phase], transform);//creates the next phase's emitter
        currentTimeLimit = phaseTimeLimits[phase];//sets the phase time limit
        healthbar.maxValue = phaseHealths[phase];//sets the phase max health
        health = phaseHealths[phase];//resets the current health to the phase max
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))//gets rid of all player bullets (one of the multiple measures I have in place to make sure the boss doesn't get bugged)
        {
            Instantiate(bulletDeathAnim, bullet.transform).transform.SetParent(null);
            Destroy(bullet);
        }
        StartCoroutine("InvulnTimer");
    }

    IEnumerator InvulnTimer()//a timer for how long the boss is invulnerable upon using a spell
    {
        invulnerable = true;
        GameObject.Find("Player").GetComponent<PlayerController>().enemyCardActive = true;//makes player unable to shoot for the duration just in case
        if (GameObject.Find("Player Emitter") != null)
        {
            GameObject.Find("Player Emitter").SetActive(false);//turns off the player's emitter in the chance that they're firing while the spell happens
        }
        yield return new WaitForSeconds(2f);
        GameObject.Find("Player").GetComponent<PlayerController>().enemyCardActive = false;//allows the player to shoot again
        invulnerable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetBossHealth : MonoBehaviour
{

    public GameObject boss;
    BossCollision bossStats;
    public Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        //boss = GameObject.Find("Boss");
        bossStats = boss.GetComponent<BossCollision>();
        healthbar.maxValue = bossStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = bossStats.health;
    }
}

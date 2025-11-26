using UnityEngine;
using UnityEngine.UI;

//画面上のHPゲージをボスの現在のHPで更新

public class GetBossHealth : MonoBehaviour
{
    //HPゲージのボス
    public GameObject boss;
    BossController bossController;

    //HPゲージ参照
    public Slider healthbar;
    
    void Start()
    {
        bossController = boss.GetComponent<BossController>();
        healthbar.maxValue = bossController.maxHealth;
    }

    void Update()
    {
        healthbar.value = bossController.health;
    }
}

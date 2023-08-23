using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Script : MonoBehaviour
{
    public GameObject boss;
    public GameObject weakEnemySpawner;
    public GameObject weakEnemyDownSpawner;
    public GameObject weakEnemyRightSpawner;
    public GameObject weakEnemyLeftSpawner;
    public GameObject moderateEnemy;
    public GameObject difficultEnemy;
    public GameObject extremeEnemy;

    void Start()
    {
        StartCoroutine(EnemyTimer());
    }

    IEnumerator EnemyTimer()
    {
        Instantiate(weakEnemySpawner).transform.position = new Vector3(-12.5f, 7f, 0f);
        yield return new WaitForSeconds(10);
        Instantiate(weakEnemyDownSpawner).transform.position = new Vector3(-7f, 10f);
        Instantiate(weakEnemyDownSpawner).transform.position = new Vector3(7f, 10f);
        yield return new WaitForSeconds(5);
        Instantiate(moderateEnemy).transform.position = new Vector3(-6f, 10f);
        yield return new WaitForSeconds(10);
        Instantiate(moderateEnemy).transform.position = new Vector3(6f, 10f);
        yield return new WaitForSeconds(7);
        Instantiate(weakEnemyRightSpawner).transform.position = new Vector3(-12.5f, 7.5f, 0f);
        Instantiate(weakEnemyLeftSpawner).transform.position = new Vector3(12.5f, 6.5f, 0f);
        Instantiate(moderateEnemy).transform.position = new Vector3(3f, 10f);
        Instantiate(moderateEnemy).transform.position = new Vector3(-3f, 10f);
        yield return new WaitForSeconds(10);
        Instantiate(moderateEnemy).transform.position = new Vector3(-7f, 10f);
        Instantiate(moderateEnemy).transform.position = new Vector3(7f, 10f);
        Instantiate(moderateEnemy).transform.position = new Vector3(-3f, 10f);
        Instantiate(moderateEnemy).transform.position = new Vector3(3f, 10f);
        yield return new WaitForSeconds(10);
        Instantiate(difficultEnemy).transform.position = new Vector3(0, 10f);
        yield return new WaitForSeconds(20);
        Instantiate(moderateEnemy).transform.position = new Vector3(-7f, 10f);
        Instantiate(moderateEnemy).transform.position = new Vector3(7f, 10f);
        yield return new WaitForSeconds(3);
        Instantiate(difficultEnemy).transform.position = new Vector3(0, 10f);
        yield return new WaitForSeconds(20);
        Instantiate(extremeEnemy).transform.position = new Vector3(0f, 10f);
        yield return new WaitForSeconds(15);
        Instantiate(difficultEnemy).transform.position = new Vector3(-7f, 10f);
        Instantiate(difficultEnemy).transform.position = new Vector3(7f, 10f);
        yield return new WaitForSeconds(20);
        Instantiate(boss);
        yield return null;
    }
}

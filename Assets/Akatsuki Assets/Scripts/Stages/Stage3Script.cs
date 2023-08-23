using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Script : MonoBehaviour
{
    public GameObject boss;
    public GameObject weakEnemy;
    public GameObject moderateEnemy;
    public GameObject difficultEnemy;
    public GameObject extremeEnemy;

    void Start()
    {
        StartCoroutine(EnemyTimer());
    }

    IEnumerator EnemyTimer()
    {
        for(int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(-6f + Random.Range(-1f, 1f), 10.5f);
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = 0;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.y = -1;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 99;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopYValue = -13;
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(6f + Random.Range(-1f, 1f), 10.5f);
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = 0;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.y = -1;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 99;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopYValue = -13;
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(5f);
        Instantiate(moderateEnemy).transform.position = new Vector3(-7f, 10.5f);
        Instantiate(moderateEnemy).transform.position = new Vector3(0f, 10.5f);
        Instantiate(moderateEnemy).transform.position = new Vector3(7f, 10.5f);
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(-12.5f, Random.Range(5f, 8f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 20;
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(10f);
        Instantiate(difficultEnemy).transform.position = new Vector3(0, 10.5f);
        yield return new WaitForSeconds(25f);
        Instantiate(extremeEnemy).transform.position = new Vector3(0, 10.5f);
        yield return new WaitForSeconds(45f);
        Instantiate(boss);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Script : MonoBehaviour
{
    public GameObject boss;
    public GameObject weakEnemy; 
    public GameObject moderateEnemy;
    public GameObject difficultEnemy;
    public GameObject extremeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyTimer());
    }

    IEnumerator EnemyTimer()
    {
        for(int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(-12.5f, Random.Range(3f, 7f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(-6f, -3f);
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(12.5f, Random.Range(3f, 7f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = -1f;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(6f, 3f);
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(-12.5f, Random.Range(3f, 7f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(-6f, -3f);
            yield return new WaitForSeconds(.25f);
        }
        for(int i = 0; i < 10; i++)
        {
            GameObject tmpEnemy = Instantiate(moderateEnemy);
            tmpEnemy.transform.position = new Vector3(12.5f, Random.Range(3f, 7f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = -1f;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(6f, 3f);
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < 10; i++)
        {
            GameObject tmpEnemy = Instantiate(moderateEnemy);
            tmpEnemy.transform.position = new Vector3(-12.5f, Random.Range(3f, 7f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(-6f, -3f);
            yield return new WaitForSeconds(.25f);
        }
        yield return new WaitForSeconds(10f);
        for (int i = -10; i <= 10; i++)
        {
            GameObject tmpEnemy = Instantiate(moderateEnemy);
            tmpEnemy.transform.position = new Vector3(i, 10.5f);
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = 0;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.y = -2;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 99;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopYValue = Random.Range(6f, 9f);
            yield return new WaitForSeconds(10/21f);
        }
        yield return new WaitForSeconds(15f);
        Instantiate(extremeEnemy).transform.position = new Vector3(0, 10.5f);
        yield return new WaitForSeconds(40);
        Instantiate(boss);
    }
}

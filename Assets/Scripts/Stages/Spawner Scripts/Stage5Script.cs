using System.Collections;
using UnityEngine;

//ステージ5の敵スポーンタイミングと位置スクリプト

public class Stage5Script : MonoBehaviour
{
    //スポーンするプレハブ
    public GameObject boss;
    public GameObject weakEnemy;
    public GameObject moderateEnemy;
    public GameObject difficultEnemy;
    public GameObject extremeEnemy;
    public GameObject mage;

    //スポーンシーケンスを起動
    void Start()
    {
        StartCoroutine(EnemyTimer());
    }

    //スポーンシーケンス
    IEnumerator EnemyTimer()
    {
        for(int i = 0; i<20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(-12.5f, Random.Range(4f, 6f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(-5f, -3f);
            yield return new WaitForSeconds(.0625f);
        }
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 20; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(12.5f, Random.Range(4f, 6f));
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = -1f;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = Random.Range(5f, 3f);
            yield return new WaitForSeconds(.0625f);
        }
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 50; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(Random.Range(-11.5f,11.5f), 10.5f);
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = 0;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.y = -2;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 99;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopYValue = Random.Range(6f,9f);
            yield return new WaitForSeconds(.0625f);
        }
        yield return new WaitForSeconds(5);
        GameObject tmpModerateEnemy = Instantiate(moderateEnemy);
        tmpModerateEnemy.transform.position = new Vector3(-12.5f, 7f);
        yield return new WaitForSeconds(.5f);
        tmpModerateEnemy = Instantiate(moderateEnemy);
        tmpModerateEnemy.transform.position = new Vector3(12.5f, 7f);
        tmpModerateEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = -1;
        tmpModerateEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 7f;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 25; i++)
        {
            GameObject tmpEnemy = Instantiate(weakEnemy);
            tmpEnemy.transform.position = new Vector3(Random.Range(-11.5f, 11.5f), 10.5f);
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.x = 0;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().moveDir.y = -2;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopXValue = 99;
            tmpEnemy.GetComponent<MoveOnSpawnAndStop>().stopYValue = Random.Range(6f, 9f);
            yield return new WaitForSeconds(.0625f);
        }
        yield return new WaitForSeconds(10);
        Instantiate(difficultEnemy).transform.position = new Vector3(0, 10.5f);
        yield return new WaitForSeconds(20);
        Instantiate(mage).transform.position = new Vector3(-7, 10.5f);
        Instantiate(mage).transform.position = new Vector3(0, 10.5f);
        Instantiate(mage).transform.position = new Vector3(7, 10.5f);
        yield return new WaitForSeconds(30);
        Instantiate(difficultEnemy).transform.position = new Vector3(0, 10.5f);
        Instantiate(moderateEnemy).transform.position = new Vector3(-12.5f, 7f);
        yield return new WaitForSeconds(7);
        GameObject tmp1 = Instantiate(moderateEnemy);
        tmp1.transform.position = new Vector3(12.5f, 7f);
        tmp1.GetComponent<MoveOnSpawnAndStop>().moveDir.x = -1;
        tmp1.GetComponent<MoveOnSpawnAndStop>().stopXValue = 7f;
        yield return new WaitForSeconds(20);
        Instantiate(extremeEnemy).transform.position = new Vector3(0, 10.5f);
        yield return new WaitForSeconds(35);
        Instantiate(boss);
        yield return null;
    }
}

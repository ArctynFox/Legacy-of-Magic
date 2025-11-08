using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Script : MonoBehaviour
{
    public GameObject boss;
    public GameObject weakBook;
    public GameObject book;

    void Start()
    {
        StartCoroutine(EnemyTimer());
    }

    IEnumerator EnemyTimer()
    {
        yield return new WaitForSeconds(4);
        Instantiate(weakBook).transform.position = new Vector3(0, 7f);
        yield return new WaitForSeconds(10f);
        Instantiate(weakBook).transform.position = new Vector3(-6, 7);
        Instantiate(weakBook).transform.position = new Vector3(6, 7);
        yield return new WaitForSeconds(15);
        Instantiate(book).transform.position = new Vector3(0, 7);
        yield return new WaitForSeconds(15);
        Instantiate(boss);
        yield return null;
    }
}

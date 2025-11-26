using System.Collections;
using UnityEngine;

//GameObjectに追加すると、指定秒数の後、または画面外に出た場合、削除する

public class DestroyAfterTime : MonoBehaviour
{

    public float seconds = 22f;
    
    void Start()
    {
        StartCoroutine(destroyer());
    }

    void FixedUpdate()
    {
        //画面買いに出た場合、削除する
        if(transform.position.x < -20f || transform.position.x > 20f || transform.position.y < -11f || transform.position.y > 11f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator destroyer()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

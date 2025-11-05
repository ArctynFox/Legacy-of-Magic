using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float seconds = 22f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyer());
    }

    void FixedUpdate()
    {
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

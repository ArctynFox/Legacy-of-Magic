using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public GameObject destroyEffect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(destroyEffect, other.transform).transform.SetParent(null);
            Destroy(other.gameObject);
        }
    }
}

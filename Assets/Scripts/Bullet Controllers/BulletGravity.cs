using UnityEngine;

//’e‚É’Ç‰Á‚·‚é‚ÆAd—Í‚Ì‰e‹¿‚ğó‚¯‚é

[RequireComponent(typeof(MoveDanmaku))]
public class BulletGravity : MonoBehaviour
{
    public float timeMultiplier = 1f;
    public float endDegree = -90f;
    public MoveDanmaku movementScript;
    public float gravity = -9.81f;

    public Vector3 velocity;

    void FixedUpdate()
    {
        velocity.y += gravity * timeMultiplier * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
    }
}

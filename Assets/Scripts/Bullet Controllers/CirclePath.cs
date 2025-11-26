using UnityEngine;

//’e‚É’Ç‰Á‚·‚é‚ÆAGameObject‚ª‰~‚ð•`‚­‚æ‚¤‚É”ò‚Ô

[RequireComponent(typeof(MoveDanmaku))]
public class CirclePath : MonoBehaviour
{
    //’e‚Ìƒx[ƒXˆÚ“®ƒXƒNƒŠƒvƒg
    public MoveDanmaku moveScript;

    //‰ñ‚·•b”
    public float loopTime = 3f;

    //ƒ[ƒh‚©‚ç‰ñ‚µŽn‚ß‚é‚Ì•b”
    public float startTime = 2f;
    float frameNumber = 0;
    Vector3 direction;

    void Start()
    {
        direction = moveScript.direction;
    }

    //Œ»Ý‚Í‰ñ‚·ŽžŠÔ‚©‚Ç‚¤‚©
    bool IsInSpinTimespan()
    {
        return frameNumber >= startTime * (int)(1 / Time.fixedDeltaTime) && frameNumber <= (startTime + loopTime) * (int)(1 / Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        float angle;
        //‰ñ‚·ŽžŠÔˆÈ“à‚¾‚Á‚½‚ç‰ñ‚»‚¤
        if(IsInSpinTimespan())
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - (7.2f / loopTime);
            direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            moveScript.direction = direction;
        }
        frameNumber++;
    }
}

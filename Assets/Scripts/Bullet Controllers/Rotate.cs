using UnityEngine;

//GameObjectに追加すると、回転する

public class Rotate : MonoBehaviour
{
    //角度差ベクトル
    public Vector3 rotationDegreesPerFrame = new Vector3(0, 0, 1f);

    void FixedUpdate()
    {
        transform.Rotate(rotationDegreesPerFrame);
    }
}

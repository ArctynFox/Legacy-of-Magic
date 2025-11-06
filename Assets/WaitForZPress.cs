using UnityEngine;

public class WaitForZPress : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            Parameters.singleton.NextStage();
        }
    }
}

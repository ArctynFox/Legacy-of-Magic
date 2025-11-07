using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    public int framesUntilDisable = 10;
    int framesSinceInstantiation = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        framesSinceInstantiation++;
        if(framesSinceInstantiation == framesUntilDisable)
        {
            gameObject.SetActive(false);
        }
    }
}

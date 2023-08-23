using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBGMTitle : MonoBehaviour
{
    Parameters param;
    bool hasUpdated;
    public string bossTrackTitle;
    // Start is called before the first frame update
    void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasUpdated)
        {
            if (param.isBoss)
            {
                GetComponent<Text>().text = bossTrackTitle;
                GetComponentInParent<Text>().text = bossTrackTitle;
                hasUpdated = true;
            }
        }
    }
}

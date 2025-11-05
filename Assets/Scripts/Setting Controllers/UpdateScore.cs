using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{

    Parameters param;
    public Text scoreG;
    public Text scoreY;
    // Start is called before the first frame update
    void Start()
    {
        param = GameObject.Find("gameParameters").GetComponent<Parameters>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreG.text = param.score.ToString();
        scoreY.text = param.score.ToString();
    }
}

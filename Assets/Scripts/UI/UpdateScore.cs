using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{

    public Text scoreG;
    public Text scoreY;

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreG.text = Parameters.singleton.score.ToString();
        scoreY.text = Parameters.singleton.score.ToString();
    }
}

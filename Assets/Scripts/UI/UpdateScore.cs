using UnityEngine;
using UnityEngine.UI;

//がっ免状のスコアを現在のスコアに更新

public class UpdateScore : MonoBehaviour
{

    public Text scoreG;
    public Text scoreY;

    void FixedUpdate()
    {
        scoreG.text = Parameters.singleton.score.ToString();
        scoreY.text = Parameters.singleton.score.ToString();
    }
}

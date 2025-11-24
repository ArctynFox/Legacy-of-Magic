using UnityEngine;
using UnityEngine.UI;

//ボスが現れたらボス戦BGM名の表示のテキストを変える

public class UpdateBGMTitle : MonoBehaviour
{
    bool hasUpdated = false;
    public string bossTrackTitle;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasUpdated)
        {
            return;
        }

        if (Parameters.singleton.isBoss)
        {
            GetComponent<Text>().text = bossTrackTitle;
            GetComponentInParent<Text>().text = bossTrackTitle;
            hasUpdated = true;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

//フェードアウト用
//フェードアウトUIオブジェクトをパラメタースクリプトに送る

public class SendFadeToParam : MonoBehaviour
{
    private void Start()
    {
        Parameters.singleton.fadeTransition = GetComponent<Image>();
    }
}

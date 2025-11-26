using UnityEngine;
using UnityEngine.UI;

//フェードアウト用
//フェードアウトUIオブジェクトをパラメタースクリプトに送る

public class SendFadeTransitionImageToParam : MonoBehaviour
{
    //フェードアウト遷移画像の初期不透明度
    [Tooltip("Opacity from 0.0 (invisible) to 1.0 (visible) to set the fade UI element to initially.")]
    public float startingOpacity = 1;

    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, startingOpacity);
        Parameters.singleton.fadeTransition = GetComponent<Image>();
    }
}

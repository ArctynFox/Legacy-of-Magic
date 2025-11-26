using UnityEngine;

//コンティニュー画面用
//コンティニュー画面と最初にホバー状態にあるボタンをパラメタースクリプトに送って自分を無効にする

public class SendContinueMenuToParams : MonoBehaviour
{
    void Awake()
    {
        Parameters.singleton.continueFirstButton = gameObject;
        Parameters.singleton.continueScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}

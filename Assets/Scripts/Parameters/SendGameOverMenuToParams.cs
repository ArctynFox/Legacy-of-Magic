using UnityEngine;

//ゲームオーバー画面用
//ゲームオーバー画面と最初にホバー状態にあるボタンをパラメタースクリプトに送って自分を無効にする

public class SendGameOverMenuToParams : MonoBehaviour
{
    private void Awake()
    {
        Parameters.singleton.gameOverFirstButton = gameObject;
        Parameters.singleton.gameOverScreen = transform.parent.gameObject;
        transform.parent.gameObject.SetActive(false);
    }
}

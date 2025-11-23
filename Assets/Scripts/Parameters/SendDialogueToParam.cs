using UnityEngine;

//ダイアログ用
//ボス戦前のダイアログをパラメタースクリプトに送る

public class SendDialogueToParam : MonoBehaviour
{
    void Awake()
    {
        Parameters.singleton.bossDialoguePre = transform.parent.gameObject;
    }
}

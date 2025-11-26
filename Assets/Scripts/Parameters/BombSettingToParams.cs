using UnityEngine;
using UnityEngine.UI;

//タイトル画面の設定用
//ライフ当たりのスペルカード数の設定をパラメタースクリプトに送る

public class BombSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider bombSlider;

    void Update()
    {
        Parameters.singleton.bombSetting = (int)bombSlider.value;
    }
}
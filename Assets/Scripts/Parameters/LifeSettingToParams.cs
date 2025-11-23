using UnityEngine;
using UnityEngine.UI;

//タイトル画面の設定用
//コンティニュー当たりのライフ数の設定をパラメタースクリプトに送る

public class LifeSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider lifeSlider;

    void Update()
    {
        Parameters.singleton.lifeSetting = (int)lifeSlider.value;
    }
}

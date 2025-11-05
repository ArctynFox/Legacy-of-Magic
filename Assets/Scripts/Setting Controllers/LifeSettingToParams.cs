using UnityEngine;
using UnityEngine.UI;

public class LifeSettingToParams : MonoBehaviour
{
    public GameObject paramObject;
    public Slider lifeSlider;
    Parameters param;

    private void Start()
    {
        param = paramObject.GetComponent<Parameters>();
    }

    // Update is called once per frame
    void Update()
    {
        param.lifeSetting = (int)lifeSlider.value;
    }
}

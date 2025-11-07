using UnityEngine;

public class FadeStageBGM : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 2f;

    //Fade the stage BGM when this script is loaded into the scene
    //For use with pre-boss dialogue prefab
    private void Awake()
    {
        if (StageBGM.singleton != null && fadeTime > 0f)
        {
            StageBGM.singleton.Fade(fadeTime);
        }
    }
}

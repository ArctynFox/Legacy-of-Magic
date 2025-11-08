using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StageBGM : MonoBehaviour
{
    public static StageBGM singleton;
    [SerializeField]
    AudioSource audioSource;

    //standard singleton pattern
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else singleton = this;
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    //public accessor for FadeAudioEnum that verifies that there is an attached audio source
    public void Fade(float seconds)
    {
        if (audioSource != null)
        {
            StartCoroutine(FadeAudioEnum(seconds));
        }
    }

    //Fade the audio source from full to no volume over a specified number of seconds and then stop playback
    IEnumerator FadeAudioEnum(float seconds)
    {
        float initialVolume = audioSource.volume;
        float elapsed = 0;
        while (elapsed < seconds)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            audioSource.volume = Mathf.Clamp(initialVolume - (initialVolume * (elapsed / seconds)), 0f, 1f);
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}

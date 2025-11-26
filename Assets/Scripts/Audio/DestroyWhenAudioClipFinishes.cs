using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyWhenAudioClipFinishes : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void Play()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(WaitForAudioClipToFinish());
    }

    IEnumerator WaitForAudioClipToFinish()
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        Destroy(gameObject);
    }
}

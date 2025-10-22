using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip, float fadeTime = 1f)
    {
        StartCoroutine(FadeInMusic(clip, fadeTime));
    }

    public void StopMusic(float fadeTime = 1f)
    {
        StartCoroutine(FadeOutMusic(fadeTime));
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private IEnumerator FadeInMusic(AudioClip newClip, float duration)
    {
        if (musicSource.isPlaying)
            yield return FadeOutMusic(duration);

        musicSource.clip = newClip;
        musicSource.Play();
        musicSource.volume = 0;

        float time = 0;
        while (time < duration)
        {
            musicSource.volume = Mathf.Lerp(0, 1, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        musicSource.volume = 1;
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;
        float time = 0;

        while (time < duration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }
}


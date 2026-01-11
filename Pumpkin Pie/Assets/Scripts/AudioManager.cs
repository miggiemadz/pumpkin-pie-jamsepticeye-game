using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement
{
    /// <summary>
    /// Simple audio manager that handles music and SFX playback with optional crossfade.
    /// Persists as a singleton across scenes.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField]
        private AudioSource musicSource; // AudioSource used for background music
        [SerializeField]
        private AudioSource sfxSource; // AudioSource used for sound effects

        private void Awake()
        {
            // Singleton pattern to persist audio manager
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Play a music clip with an optional fade-in. If another track is playing it will fade out first.
        /// </summary>
        /// <param name="clip">Music clip to play.</param>
        /// <param name="fadeTime">Duration of fade in seconds.</param>
        public void PlayMusic(AudioClip clip, float fadeTime = 1f)
        {
            StartCoroutine(FadeInMusic(clip, fadeTime));
        }

        /// <summary>
        /// Fade out current music over the given duration.
        /// </summary>
        /// <param name="fadeTime">Duration of fade in seconds.</param>
        public void StopMusic(float fadeTime = 1f)
        {
            StartCoroutine(FadeOutMusic(fadeTime));
        }

        /// <summary>
        /// Play a one-shot SFX immediately.
        /// </summary>
        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Fade in the provided music clip. If a track is currently playing it will first fade out.
        /// </summary>
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

        /// <summary>
        /// Fade out the current music to silence then stop playback.
        /// </summary>
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
}


using UnityEngine;

namespace Script.Common
{
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        public AudioSource backgroundMusicSource;
        public AudioSource soundEffectSource;

        [Header("Audio Clips")]
        public AudioClip defaultBGM;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (defaultBGM != null)
            {
                PlayBackgroundMusic(defaultBGM);
            }
        }

        public void PlayBackgroundMusic(AudioClip clip, bool loop = true)
        {
            if (backgroundMusicSource == null) return;

            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = loop;
            backgroundMusicSource.Play();
        }

        public void StopBackgroundMusic()
        {
            if (backgroundMusicSource == null) return;

            backgroundMusicSource.Stop();
        }

        public void PlaySoundEffect(AudioClip clip)
        {
            if (soundEffectSource == null) return;

            soundEffectSource.PlayOneShot(clip);
        }

        public void SetBGMVolume(float volume)
        {
            if (backgroundMusicSource != null)
                backgroundMusicSource.volume = Mathf.Clamp01(volume);
        }

        public void SetSfxVolume(float volume)
        {
            if (soundEffectSource != null)
                soundEffectSource.volume = Mathf.Clamp01(volume);
        }
    }

}

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;
    }

    [Header("Audio Settings")]
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    private bool isMusicPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isMusicPlaying && !musicSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void StartPlaylist()
    {
        if (musicSounds.Length > 0)
        {
            isMusicPlaying = true;
            PlayRandomMusic();
        }
        else
        {
            return;
        }
    }

    private void PlayRandomMusic()
    {
        int randomIndex = UnityEngine.Random.Range(0, musicSounds.Length);
        Sound randomSound = musicSounds[randomIndex];

        musicSource.clip = randomSound.clip;
        musicSource.volume = randomSound.volume;
        musicSource.Play();
    }

    public void PlaySFX(string soundName)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == soundName);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.Log($"SFX sound '{soundName}' not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

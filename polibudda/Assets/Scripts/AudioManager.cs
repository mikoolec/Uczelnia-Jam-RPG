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
    private int lastPlayedIndex = -1;

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
        if (isMusicPlaying && musicSource != null && !musicSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    public void StartPlaylist()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicSource is not assigned!");
            return;
        }

        if (musicSounds == null || musicSounds.Length == 0)
        {
            Debug.LogError("No music sounds assigned!");
            return;
        }

        isMusicPlaying = true;

        Debug.Log("Starting music playlist...");
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        int randomIndex;

        if (musicSounds.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            do
            {
                randomIndex = UnityEngine.Random.Range(0, musicSounds.Length);
            }
            while (randomIndex == lastPlayedIndex);
        }

        lastPlayedIndex = randomIndex;

        Sound randomSound = musicSounds[randomIndex];

        if (randomSound.clip == null)
        {
            Debug.LogError("Selected music clip is null!");
            return;
        }

        musicSource.clip = randomSound.clip;
        musicSource.volume = randomSound.volume;
        musicSource.Play();

        Debug.Log("Playing: " + randomSound.name);
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

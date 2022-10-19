using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] sounds;

    [SerializeField] private AudioMixerGroup musicMixer, sfxMixer;

    [SerializeField] private Sound[] progressMusic;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }



            DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            switch (s.soundType)
            {
                case SoundType.Music:
                    s.source.outputAudioMixerGroup = musicMixer;
                    break;
                case SoundType.Sfx:
                    s.source.outputAudioMixerGroup = sfxMixer;
                    break;
            }
        }
    }

    private bool musicPlaying;
    
    private void Start()
    {
        Play("main_theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s != null)
        {
            if (s.soundType == SoundType.Music && !musicPlaying)
            {
                s.source.Play();
                musicPlaying = true;
                StartCoroutine(StopPlaying(s.clip.length));
            }
            else if (s.soundType == SoundType.Sfx)
                s.source.Play();
        }
        else
            Debug.LogError("Sound not found!");
    }

    IEnumerator StopPlaying(float songDuration)
    {
        yield return new WaitForSeconds(songDuration);
        musicPlaying = false;
    }

    public void ProgressMusic()
    {
        if (progressMusic.Length == 0)
        {
            Debug.LogError("Music must still be added!");
            return;
        }

        Play(progressMusic[UnityEngine.Random.Range(0, progressMusic.Length)].name);
    }
}

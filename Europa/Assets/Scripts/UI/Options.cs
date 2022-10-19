using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [Header("Audio")]
    private float masterV, musicV, sfxV;
    [SerializeField] private Slider masterS, musicS, sfxS;


    private void Update()
    {
        masterV = masterS.value;
        musicV = musicS.value;
        sfxV = sfxS.value;

        SetVolume();
    }

    public AudioMixer mixer;

    private void SetVolume()
    {
        mixer.SetFloat("MasterVolume", masterV);
        mixer.SetFloat("MusicVolume", musicV);
        mixer.SetFloat("SfxVolume", sfxV);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscren(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private float musicVolume;
    private float sfxVolume;

    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource musicSource;

    private void Start()
    {
        musicVolume = 1.0f;
        sfxVolume = 1.0f;

        // Add listeners to the sliders and invokes a method when the value changes.
        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;

        if (sfxVolume > 1.0f)
            sfxVolume = 1.0f;
        else if (sfxVolume < 0.0f)
            sfxVolume = 0.0f;

        Debug.Log("SFX Volume: " + sfxVolume);
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;

        if (musicVolume > 1.0f)
            musicVolume = 1.0f;
        else if (musicVolume < 0.0f)
            musicVolume = 0.0f;

        Debug.Log("Music Volume: " + musicVolume);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip, sfxVolume);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        musicSource.clip = music;
        musicSource.Play();
    }
}

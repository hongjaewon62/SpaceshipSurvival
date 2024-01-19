using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] bgmSounds, sfxSounds;
    public AudioSource bgmSource, sfxSource;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("bgmVolume"))
        {
            PlayerPrefs.SetFloat("bgmVolume", 1);
        }
        else
        {
            LoadBgmVolume();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
        }
        else
        {
            LoadSfxVolume();
        }


        PlayBgm("IntroBgm");
    }

    public void PlayBgm(string name)
    {
        Sound sound = Array.Find(bgmSounds, x => x.name == name);

        if(sound == null)
        {
            Debug.Log("Not Found");
        }
        else
        {
            bgmSource.clip = sound.clip;
            bgmSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void BgmVolume(float volume)
    {
        bgmSource.volume = volume;
        SaveBgmVolume();
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveSfxVolume();
    }

    private void SaveBgmVolume()
    {
        PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
    }

    private void SaveSfxVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    private void LoadBgmVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
    }

    private void LoadSfxVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] bgmSounds, sfxSounds;
    public AudioSource bgmSource, sfxSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

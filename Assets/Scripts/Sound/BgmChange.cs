using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmChange : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void IntroSound()
    {
        audioSource.clip = audioClip[0];
        audioSource.Play();
    }

    public void MainSound()
    {
        audioSource.clip = audioClip[1];
        audioSource.Play();
    }
}

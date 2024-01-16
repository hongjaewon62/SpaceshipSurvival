using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUi : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    public void BgmVolume()
    {
        SoundManager.instance.BgmVolume(bgmSlider.value);
    }

    public void SfxVolume()
    {
        SoundManager.instance.SfxVolume(sfxSlider.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Slider BGMVolume;
    public Slider SFXVolume;
    public AudioSource bGMSource;
    public AudioSource sfxSource;
    public AudioSource clickSource;

    private float bgmvol = 1;
    private float sfxvol = 1;

    private void Start()
    {
        SettingVol();   
    }

    public void BGMSoundSlider()
    {
        bGMSource.volume = BGMVolume.value;

        bgmvol = BGMVolume.value;
        PlayerPrefs.SetFloat("BGMvol", bgmvol);
    }
    
    public void SFXSoundSlider()
    {
        sfxSource.volume = SFXVolume.value;

        sfxvol = SFXVolume.value;
        PlayerPrefs.SetFloat("SFXvol", sfxvol);
    }

    public void ClickSound()
    {
        clickSource.Play();
    }
    
    void SettingVol()
    {
        bgmvol = PlayerPrefs.GetFloat("BGMvol", 1);
        BGMVolume.value = bgmvol;
        bGMSource.volume = BGMVolume.value;

        sfxvol = PlayerPrefs.GetFloat("SFXvol", 1);
        SFXVolume.value = sfxvol;
        sfxSource.volume = SFXVolume.value;
    }


}

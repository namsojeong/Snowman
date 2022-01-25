using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundM : MonoBehaviour
{
    public static SoundM Instance = null;

    [Header("VolumeSlider")]
    [SerializeField]
    Slider BGMVolume;
    [SerializeField]
    Slider SFXVolume;
    
    [Header("AudioSourcce")]
    [SerializeField]
    AudioSource bGMSource;
    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioSource clickSource;

    [Header("AudioClip")]
    [SerializeField]
    AudioClip[] bgmClip;
    [SerializeField]
    AudioClip[] sfxClip;
    [SerializeField]
    AudioClip[] clickClip;


    private float bgmvol = 1;
    private float sfxvol = 1;

    private void Awake()
    {
        Instance = this;
        if (Instance == null)
            Instance = GetComponent<SoundM>();


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
        clickSource.clip = clickClip[0];
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

    //사운드 실행
    public void SoundOn(string source, int num)
    {
        if (source == "SFX")
        {
            sfxSource.clip = sfxClip[num];
            sfxSource.Play();
        }
        else if (source == "BGM")
        {
            bGMSource.clip = bgmClip[num];
            bGMSource.Play();
        }
        else if (source == "COIN")
        {
            clickSource.clip = sfxClip[4];
            clickSource.Play();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Slider backVolume;
    public AudioSource audio;

    private float backvol = 1;

    private void Start()
    {
        backvol = PlayerPrefs.GetFloat("backvol", 1);
        backVolume.value = backvol;
        audio.volume = backVolume.value;
    }
    private void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        audio.volume = backVolume.value;

        backvol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backvol);
    }
}

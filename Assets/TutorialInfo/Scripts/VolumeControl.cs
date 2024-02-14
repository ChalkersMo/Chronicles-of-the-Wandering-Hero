using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MusicVolume";
    public AudioMixer mixer;
    public Slider slider;

    private const float _multiplier = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HadleSliderValueChanged);
    }

    private void HadleSliderValueChanged(float value)
    {
        var volumeValue = Mathf.Log10(value) * _multiplier;
        mixer.SetFloat(volumeParameter, volumeValue);   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}

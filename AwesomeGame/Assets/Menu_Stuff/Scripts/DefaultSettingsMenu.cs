using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;//для настроек громкости, для взаимодествия с аудиомиксером
using UnityEngine.UI;//для настроек разрешения, для 
using TMPro;

public class DefaultSettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    [SerializeField] Slider volumeslider;
    [SerializeField] TMP_Dropdown qualityDropdown;
    #region Work_with_Settings
    private void Start()
    {

    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion
    #region Default_and_Prefs
    //////////////////////////////////////////////////////////////////////////// Setting Default Values
    //Default values
    const float DEFAULT_VOLUME_VALUE = -15f;
    const int DEFAULT_QUALITY_VALUE = 2;
    //Prefs keys
    const string MASTER_VOLUME_KEY = "master volume";
    const string QUALITY_KEY = "quality settings";
    
    public void SetDefaultSettings()
    {
        SetDefaultVolume();
        SetDefaultQuality();
    }

    public void SetDefaultVolume()
    {
        Debug.Log(DEFAULT_VOLUME_VALUE);
        audioMixer.SetFloat("volume", DEFAULT_VOLUME_VALUE);
        volumeslider.value = DEFAULT_VOLUME_VALUE;
    }

    public void SetDefaultQuality()
    {
        QualitySettings.SetQualityLevel(DEFAULT_QUALITY_VALUE);
        qualityDropdown.value = DEFAULT_QUALITY_VALUE;
    }

    public void SaveSettingstoPrefs()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volumeslider.value);
        PlayerPrefs.SetInt(QUALITY_KEY, qualityDropdown.value);
    }
    #endregion
}

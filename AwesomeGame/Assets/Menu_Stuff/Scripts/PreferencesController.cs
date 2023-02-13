using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesController : MonoBehaviour
{
    //PREFS PARAMETRS
    const string MASTER_VOLUME_KEY = "master volume";
    const string QUALITY_INDEX_KEY = "quality index";
    //DEFAULT VALUES
    const float DEFAULT_VOLUME_VALUE = -15f;
    //SETTERS


    public float GetDefaultVolumeValue()
    {
        return DEFAULT_VOLUME_VALUE;
    }

    public static void SetDefaultVolumeValue()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, DEFAULT_VOLUME_VALUE);

        FindObjectOfType<DefaultSettingsMenu>().SetVolume(DEFAULT_VOLUME_VALUE);
    }
}

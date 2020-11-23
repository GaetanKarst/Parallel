using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager 
{
    const string Master_Volume_Key = "Master Volume";

    const float Min_Volume = 0f;
    const float Max_volume = 1f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= Min_Volume && volume <= Max_volume)
        {
            PlayerPrefs.SetFloat(Master_Volume_Key, volume);
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(Master_Volume_Key);
    }
}

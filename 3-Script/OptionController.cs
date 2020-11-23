using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = defaultVolume;
    }

    // Update is called once per frame
    void Update()
    {
        var audioManager = FindObjectOfType<AudioManager>();
        if (audioManager)
        {
            audioManager.SetVolume(volumeSlider.value);
        }
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        FindObjectOfType<GameManager>().LoadMainMenu();
    }
    
    public void SetDefault()
    {
        volumeSlider.value = defaultVolume;
    }
}

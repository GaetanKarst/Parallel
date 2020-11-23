using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }
    //s[SerializeField] AudioClip _winSound;
    [SerializeField] AudioClip _deathSound;

    private int _deathAudioCount;

    AudioSource _audioSource;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else if (_instance != this)
            Destroy(gameObject);

        KeepTheIntanceAtNextLevel();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;//Music not by default at start
    }

    private void KeepTheIntanceAtNextLevel()
    {
        int numAudioManager = FindObjectsOfType<AudioManager>().Length;
        if (numAudioManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void PlayDeathSound()
    {
        if (_deathAudioCount == 1)//fix so that the sound does not play twice
            return;

        if (_deathAudioCount < 1)
        {
            _audioSource.PlayOneShot(_deathSound);
            _deathAudioCount++;
        }
        StartCoroutine(ResetAudioCount());
    }

    IEnumerator ResetAudioCount()
    {
        yield return new WaitForSeconds(2f);
        _deathAudioCount = 0;
    }

    /*public void PlayWinSound()
   {
       if(_deathAudioCount == 0)
       _audioSource.PlayOneShot(_winSound);
   }*/
}

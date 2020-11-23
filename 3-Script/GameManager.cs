using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float resetDelay = 2f;
    
    private int _currentSceneIndex;

    AudioManager _gameSfx;

    [SerializeField] ParticleSystem _winVfx;

    private void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _gameSfx = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_gameSfx && !_winVfx) { return; }//bug fix

        if (collision.gameObject.tag == "Friendly")
        {
            StartCoroutine(StartSuccessSequence());
        }
    }

    IEnumerator StartSuccessSequence()
    {
        //AudioManager.Instance.PlayWinSound();
        _winVfx.Play();
        yield return new WaitForSeconds(resetDelay);
        LoadNextScene();//split so that we can use the method otherwise if needed
    }
    public void LoadNextScene()
    {
        if (_currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            LoadMainMenu();
            return;
        }

        SceneManager.LoadScene(_currentSceneIndex + 1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ResetScene()
    {
        StartCoroutine(ResetRoutine());
        
    }

    IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(resetDelay);
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //Variables
    [SerializeField] float _mainThrust = 50f;
    [SerializeField] float _rcsThrust = 100f;
    [SerializeField] AudioClip _mainEngine;
    [SerializeField] ParticleSystem _deathVfx;
    [SerializeField] ParticleSystem _thrustVfx;

    //Cache data
    Rigidbody _myRigidbody;
    AudioSource _audioSource;
    GameManager gameManager;

    //states

    public bool isPlayerAlive = true;
    public bool isCollisionEnabled = true;

    private void Awake() 
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerAlive)
        return;
       
        Rotate();

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            gameManager.LoadNextScene();
        }

        if (Input.GetKey(KeyCode.C))
        {
            isCollisionEnabled = !isCollisionEnabled;
        }
    }

    private void FixedUpdate()
    {
        RespondToThrust();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( !isPlayerAlive || !isCollisionEnabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                return;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Untagged":
                StartDeathSequence();
                break;
            default:
                break;
        }
    }

    private void StartDeathSequence()
    {
        isPlayerAlive = false;
        AudioManager.Instance.PlayDeathSound();
        _deathVfx.Play();
        gameManager.ResetScene();
        
    }

    private void Rotate()
    {
        
        float rotationThisFrame = _rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            RotateManually(rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateManually(-rotationThisFrame);
        }
    }

    private void RotateManually(float rotationThisFrame)
    {
        _myRigidbody.freezeRotation = true;//takes manual control of rotation
        transform.Rotate(rotationThisFrame * Vector3.forward);
        _myRigidbody.freezeRotation = false;//resume physics control of rotation
    }

    private void RespondToThrust()
    {

        if (Input.GetKey(KeyCode.Space) && isPlayerAlive == true)
        {
            ApplyThrust();
        }
            
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        _audioSource.Stop();
        _thrustVfx.Stop();
    }

    private void ApplyThrust()
    {
        _myRigidbody.AddRelativeForce(_mainThrust * Vector3.up * Time.deltaTime); 

        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(_mainEngine);

        _thrustVfx.Play();
    }
}

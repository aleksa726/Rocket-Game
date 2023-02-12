using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float force = 100f;
    [SerializeField] float rotation = 100f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    Rigidbody playerRigidbody;
    AudioSource audioSource;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        ThrustLogic();
        RotationLogic();
    }

    private void ThrustLogic()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ThrustStart();
        }
        else
        {
            ThrustStop();
        }
    }

    private void RotationLogic()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    private void RotateRight()
    {
        Rotate(1);
        if (!leftBooster.isPlaying)
            leftBooster.Play();
    }

    private void RotateLeft()
    {
        Rotate(-1);
        if (!rightBooster.isPlaying)
            rightBooster.Play();
    }

    private void ThrustStop()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void ThrustStart()
    {
        playerRigidbody.AddRelativeForce(Vector3.up * force * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound, 0.4f);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void Rotate(int direction)
    {
        playerRigidbody.freezeRotation = true;
        transform.Rotate(direction * Vector3.forward * rotation * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }

}

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

        if (Input.GetKey(KeyCode.W))
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
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            Rotate(-1);
            if (!rightBooster.isPlaying)
                rightBooster.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(1);
            if (!leftBooster.isPlaying)
                leftBooster.Play();
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }


    void Rotate(int direction)
    {
        playerRigidbody.freezeRotation = true;
        transform.Rotate(direction * Vector3.forward * rotation * Time.deltaTime);
        playerRigidbody.freezeRotation = false;
    }

}

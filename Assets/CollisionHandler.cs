using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    Movement movement;
    AudioSource audioSource;


    bool isEnd = false;


    private void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isEnd) return;
        switch (other.gameObject.tag)
        {
            case "Finish":
                Success();
                break;
            case "Respawn":
                break;
            case "Fuel":
                break;
            default:
                Crash();
                break;
        }
    }

    void Success()
    {
        isEnd = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("NextLevel", levelDelay);
    }

    void Crash()
    {
        isEnd = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        movement.enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel", levelDelay);
    }

    void ReloadLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex);
    }

    void NextLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currSceneIndex = (currSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(currSceneIndex);
    }
}

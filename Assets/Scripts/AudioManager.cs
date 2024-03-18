using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource audioSource2D;
    [SerializeField] private AudioClip musiqueCaroussel;

    [SerializeField] private AudioClip[] audiosPhoto;

    //[SerializeField] private float fadeDuration = 1.0f; // Durée du fondu en secondes
    void Start(){
        audioSource2D.clip = musiqueCaroussel;
        audioSource2D.loop = true; // Activer la boucle
        audioSource2D.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SonPhoto(){
        int randomNumb = UnityEngine.Random.Range(0, audiosPhoto.Length);
        audioSource2D.PlayOneShot(audiosPhoto[randomNumb]);
    }
}

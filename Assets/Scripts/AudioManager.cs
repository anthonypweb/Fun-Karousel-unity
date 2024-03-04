using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource audioSource2D;
    [SerializeField] private AudioClip[] musiquesCaroussel;

    [SerializeField] private AudioClip[] audiosPhoto;

    [SerializeField] private float fadeDuration = 1.0f; // Durée du fondu en secondes
    void Start(){
        audioSource2D.clip = musiquesCaroussel[Convert.ToInt32(gameManager.theme)];
        audioSource2D.loop = true; // Activer la boucle
        audioSource2D.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //REVOIR LA LOGIQUE POUR PLUS OPTIMAL
    public IEnumerator ChangementTheme(){
        float timer = 0f;
        float startVolume = audioSource2D.volume;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource2D.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }
        audioSource2D.Stop();
        
        audioSource2D.clip = musiquesCaroussel[Convert.ToInt32(gameManager.theme)];
        audioSource2D.loop = true; // Activer la boucle
        audioSource2D.volume = startVolume;
        audioSource2D.Play();

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource2D.volume = Mathf.Lerp(0f, startVolume, timer / fadeDuration);
            yield return null;
        }
    }
    public void SonPhoto(){
        int randomNumb = UnityEngine.Random.Range(0, audiosPhoto.Length);
        audioSource2D.PlayOneShot(audiosPhoto[randomNumb]);
    }
}

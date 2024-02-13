using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource audioSource2D;
    [SerializeField] private AudioClip[] musiquesCaroussel;
    
    void Start(){
        audioSource2D.PlayOneShot(musiquesCaroussel[Convert.ToInt32(gameManager.theme)]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //REVOIR LA LOGIQUE POUR PLUS OPTIMAL
    public void ChangementTheme(){
        audioSource2D.Stop();
        audioSource2D.PlayOneShot(musiquesCaroussel[Convert.ToInt32(gameManager.theme)]);
        
    }
}

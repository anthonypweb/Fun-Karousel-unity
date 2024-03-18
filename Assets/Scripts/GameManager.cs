using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
   
    public float rotationSpeed = 1f; // Vitesse de rotation des cheveaux
    //public bool jeTourne = false;

    [SerializeField] private AudioManager audioManager; //Variable contenant l'AudioManager
    [SerializeField] private AudioSource audioSource2D; //Variable contenant l'AudioManager

    [SerializeField] private Animator animatorCaroussel;

    [SerializeField] private VideoPlayer bgSkybox;

    // Update is called once per frame
    void Update()
    {
        
        ChangementRapidite();
        
    }


    //TON SCRIPT ICIIIII!!!
    void ChangementRapidite()
    {
        // Vérifiez si votre Animator a été correctement assigné
        if (animatorCaroussel != null)
        {
            // Utilisez votre variable rotationSpeed pour ajuster la vitesse de l'animation
            animatorCaroussel.speed = rotationSpeed;
            audioSource2D.pitch = rotationSpeed;
            bgSkybox.playbackSpeed = rotationSpeed;
        }
        else
        {
            Debug.LogError("Animator non assigné. Assurez-vous d'attacher le composant Animator à votre GameObject.");
        }
    }
    
    public void PlaySfxPhoto(){
        audioManager.SonPhoto();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables accessible publiquement pour être modifié à l'externe
    public bool jeTourne = false;
    public bool theme = true; //valeur TRUE = Cyber, FALSE = Retro
    public bool triggerChangementTheme = false;
    public float rotationSpeed = 1f; // Vitesse de rotation des cheveaux

    
    [SerializeField] private AudioManager audioManager; //Variable contenant l'AudioManager
    [SerializeField] private AudioSource audioSource2D; //Variable contenant l'AudioManager

    [SerializeField] private Animator animatorCaroussel;
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
        ChangementRapidite();
        VerifTheme(); // Fonction Appeler pour verifier le theme actif
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
        }
        else
        {
            Debug.LogError("Animator non assigné. Assurez-vous d'attacher le composant Animator à votre GameObject.");
        }
    }
    

    //Fonction appeler pour gerer les themes : Les sons et Animation/model3D
    void VerifTheme(){
        if(triggerChangementTheme) //si changement theme sur la switch
        {
            theme = !theme;
            audioManager.ChangementTheme();
            triggerChangementTheme = !triggerChangementTheme;
        }
        
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables accessible publiquement pour être modifié à l'externe
    public bool jeTourne = false;
    public bool theme = true; //valeur TRUE = Cyber, FALSE = Retro
    public bool triggerChangementTheme = false;

    [SerializeField] private GameObject centerObject; // Objet autour duquel les cheveaux doivent tourner
    [SerializeField] public float radius = 5f; // Rayon de distribution des cheveaux
    [SerializeField] public int rotationSpeed = 1; // Vitesse de rotation des cheveaux

    private GameObject[] horses; // Tableau pour stocker les cheveaux

    [SerializeField] private AudioManager audioManager; //Variable contenant l'AudioManager


    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        

        VerifTheme(); // Fonction Appeler pour verifier le theme actif
    }


    //TON SCRIPT ICIIIII!!!
    void JeTourne()
    {
        int numHorses = horses.Length;
        for (int i = 0; i < numHorses; i++)
        {
            horses[i].transform.RotateAround(centerObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    void FindAndDistributeHorses()
    {
        horses = GameObject.FindGameObjectsWithTag("Horse"); // Trouver tous les cubes avec le tag "Cube"
        int numHorses = horses.Length;
        float angleIncrement = 360f / numHorses;
        for (int i = 0; i < numHorses; i++)
        {
            float angle = i * angleIncrement;
            Vector3 offset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
            horses[i].transform.position = centerObject.transform.position + offset;
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
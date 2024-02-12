using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables accessible publiquement pour être modifié à l'externe
    public bool jeTourne = false;
    public GameObject centerObject; // Objet autour duquel les cubes doivent tourner
    public float radius = 5f; // Rayon de distribution des cubes
    public int rotationSpeed = 1; // Vitesse de rotation des cubes

    private GameObject[] horses; // Tableau pour stocker les cubes


    // Start is called before the first frame update
    void Start()
    {
        //ActivateMultiMonitors();
        FindAndDistributeHorses();

    }

    // Update is called once per frame
    void Update()
    {
        //si la variable est vrai, part l'animation (POUR TOI ANTHONY!)
        if (jeTourne)
        {
            JeTourne();
        }

    }

    void ActivateMultiMonitors()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
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

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Définir les bornes de la plage initiale et finale
    const float minPlageInitiale = 23f;
    const float maxPlageInitiale = 720f;
    const float minPlageFinale = 5f;
    const float maxPlageFinale = 60f;

    // Fonction pour convertir la valeur de la plage initiale à la plage finale
    float ConvertirPlage(float valeur)
    {
        // Appliquer une règle de trois
        return minPlageFinale + (valeur - minPlageInitiale) * (maxPlageFinale - minPlageFinale) / (maxPlageInitiale - minPlageInitiale);
    }

    // Use this for initialization
    void Start()
    {
        // Abonnez-vous à l'événement de réception des messages
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // Convertir la valeur de la chaîne en float
        float valeurCapteurAnalogique = float.Parse(msg);

        // Convertir la valeur de la plage initiale à la plage finale
        float vitesseConvertie = ConvertirPlage(valeurCapteurAnalogique);

        // Envoyer la vitesse convertie au GameManager

        gameManager.rotationSpeed = vitesseConvertie;
        Debug.Log("Valeur du capteur analogique : " + valeurCapteurAnalogique);
        Debug.Log("Vitesse convertie : " + vitesseConvertie);
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}

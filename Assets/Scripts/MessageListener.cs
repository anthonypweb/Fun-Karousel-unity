using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Définir les bornes de la plage initiale et finale
    [SerializeField] private float minPlageInitiale = 0f;
    [SerializeField]  private float maxPlageInitiale = 450f;
    [SerializeField] private float minPlageFinale = 0.1f;
    [SerializeField] private float maxPlageFinale = 1f;

    // Fonction pour convertir la valeur de la plage initiale à la plage finale
    void ConvertirPlage(float valeur)
    {
       
        // Appliquer une règle de trois avec les bornes spécifiées
        float valeurConvertie = minPlageFinale + (valeur - minPlageInitiale) * (maxPlageFinale - minPlageFinale) / (maxPlageInitiale - minPlageInitiale);


        // Appliquer une règle de trois
        gameManager.rotationSpeed = valeurConvertie;
        
        
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
    // Diviser la chaîne en deux parties en utilisant la virgule comme délimiteur
    string[] parties = msg.Split(',');
    // Maintenant, vous pouvez utiliser ces valeurs comme vous le souhaitez, par exemple les envoyer à votre GameManager
    // gameManager.boutonPresse = boutonPresse;
    // gameManager.valeurRoulette = valeurRoulette;
    float valeurRoulette = float.Parse(parties[1]);
    ConvertirPlage(valeurRoulette);  

    

    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}

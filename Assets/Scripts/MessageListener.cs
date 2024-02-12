using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMessageListener : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Définir les bornes de la plage initiale et finale
      [SerializeField] private int minPlageInitiale = 0;
     [SerializeField]  private int maxPlageInitiale = 450;
      [SerializeField] private int minPlageFinale = 5;
       [SerializeField] private int maxPlageFinale = 60;

    // Fonction pour convertir la valeur de la plage initiale à la plage finale
    void ConvertirPlage(int valeur)
    {
        if(gameManager.jeTourne){ //Si le carousel est ON
            // Appliquer une règle de trois
            gameManager.rotationSpeed = minPlageFinale + (valeur - minPlageInitiale) * (maxPlageFinale - minPlageFinale) / (maxPlageInitiale - minPlageInitiale);
        }
        
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
     
    // Vérifier si la première partie est "0" pour déterminer l'état du bouton
    if(parties[0] == "on"){
        gameManager.jeTourne = true;
        // Convertir la deuxième partie en valeur numérique pour obtenir la valeur de la roulette
    }else if(parties[0] == "off"){
        gameManager.jeTourne = false;
    }
    // Maintenant, vous pouvez utiliser ces valeurs comme vous le souhaitez, par exemple les envoyer à votre GameManager
    // gameManager.boutonPresse = boutonPresse;
    // gameManager.valeurRoulette = valeurRoulette;
    int valeurRoulette = int.Parse(parties[1]);
    ConvertirPlage(valeurRoulette);  

    // Faites ce que vous voulez avec les valeurs séparées
    //Debug.Log("Bouton pressé : " + boutonPresse);
    //Debug.Log("Valeur de la roulette : " + valeurRoulette);

    
}

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}

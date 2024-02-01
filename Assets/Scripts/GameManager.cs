using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables accessible publiquement pour être modifié à l'externe
    public bool jeTourne = false;


    // Start is called before the first frame update
    void Start()
    {
        //ActivateMultiMonitors();
    }

    // Update is called once per frame
    void Update()
    {
        //si la variable est vrai, part l'animation (POUR TOI ANTHONY!)
        if(jeTourne){
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
    void JeTourne(){
        
    }
}

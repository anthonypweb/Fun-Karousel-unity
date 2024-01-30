using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ActivateMultiMonitors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateMultiMonitors()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Renderer faceRenderer; // Référence au composant Renderer du visage du modèle 3D
    // Start is called before the first frame update
    void Start()
    {
        // Charger la texture depuis le dossier (assurez-vous que le chemin est correct)
        Texture2D texture = LoadFaceTexture("Assets/Photo/Image.jpg");

        // Appliquer la texture sur le matériau du visage
        faceRenderer.material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Texture2D LoadFaceTexture(string path)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // Charger l'image depuis le tableau d'octets
        return texture;
    }
}


using UnityEngine;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public string imageFolderPath; // Chemin du dossier contenant les images
    public GameObject cubePrefab; // Préfabriqué du cube à utiliser

    private string[] previousImages; // Liste des images déjà traitées
    private float lastCheckTime; // Dernier moment où nous avons vérifié les nouvelles images

    void Start()
    {
        // Initialiser la liste des images déjà traitées
        previousImages = Directory.GetFiles(imageFolderPath, "*.png");

        // Initialiser le dernier moment de vérification des nouvelles images
        lastCheckTime = Time.time;
    }

    void Update()
    {
        // Vérifier s'il s'est écoulé une seconde depuis la dernière vérification
        if (Time.time - lastCheckTime >= 1f)
        {
            // Mettre à jour le dernier moment de vérification
            lastCheckTime = Time.time;

            // Vérifier s'il y a de nouvelles images ajoutées au dossier
            string[] currentImages = Directory.GetFiles(imageFolderPath, "*.png");
            foreach (string imagePath in currentImages)
            {
                if (!ArrayContains(previousImages, imagePath))
                {
                    // Nouvelle image détectée, chargement de la texture et création du cube
                    byte[] fileData = File.ReadAllBytes(imagePath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = Vector3.zero; // Position du cube
                    cube.GetComponent<Renderer>().material.mainTexture = texture;
                }
            }

            // Mettre à jour la liste des images déjà traitées
            previousImages = currentImages;
        }
    }

    bool ArrayContains(string[] array, string value)
    {
        foreach (string element in array)
        {
            if (element == value)
            {
                return true;
            }
        }
        return false;
    }
}

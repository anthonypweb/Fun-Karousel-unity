using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public string imageFolderPath; // Chemin du dossier contenant les images

    private List<(Texture2D, DateTime)> textureList = new List<(Texture2D, DateTime)>(); // Liste des textures chargées avec leur date d'ajout
    private GameObject[] templateFaces; // Tableau de template pour mettre les faces

    void Start()
    {
        // Récupérer tous les objets avec le tag "TemplateFace" au démarrage
        templateFaces = GameObject.FindGameObjectsWithTag("TemplateFaces");
    }

    void Update()
    {
    // Vérifier s'il y a de nouvelles images à chaque mise à jour
    string[] currentImages = Directory.GetFiles(imageFolderPath, "*.png");
    foreach (string imagePath in currentImages)
    {
        // Charger la nouvelle image depuis le fichier
        byte[] fileData = File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Ajouter la texture à la liste avec sa date d'ajout
        textureList.Add((texture, DateTime.Now));


        // Si le nombre de textures dépasse le nombre de templateFaces, retirer la texture la plus ancienne
        if (textureList.Count > templateFaces.Length)
        {
            RemoveOldestTexture();
        }
    }

    // Mettre à jour les textures des templates
    for (int i = 0; i < templateFaces.Length; i++)
    {
        // S'assurer qu'il y a une texture associée au masque
        if (i < textureList.Count)
        {
            // Récupérer le renderer du masque
            Renderer cubeRenderer = templateFaces[i].GetComponent<Renderer>();

            // Assigner la texture au masque
            cubeRenderer.material.mainTexture = textureList[i].Item1;

            // Calculer le décalage pour centrer la texture
            Vector2 textureOffset = new Vector2(0.5f - textureList[i].Item1.width / (2f * textureList[i].Item1.width), 0.5f - textureList[i].Item1.height / (2f * textureList[i].Item1.height));
            cubeRenderer.material.mainTextureOffset = textureOffset;
        }
    }
    }

    void RemoveOldestTexture()
    {
        // Recherche de la texture la plus ancienne dans la liste
        DateTime oldestDate = DateTime.MaxValue;
        int indexToRemove = -1;
        for (int i = 0; i < textureList.Count; i++)
        {
            if (textureList[i].Item2 < oldestDate)
            {
                oldestDate = textureList[i].Item2;
                indexToRemove = i;
            }
        }

        // Suppression de la texture la plus ancienne de la liste
        if (indexToRemove >= 0)
        {
            Texture2D textureToRemove = textureList[indexToRemove].Item1;
            Destroy(textureToRemove); // Libérer la mémoire en détruisant la texture
            textureList.RemoveAt(indexToRemove);
        }
    }
}

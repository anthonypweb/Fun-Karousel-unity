using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public string imageFolderPath; // Chemin du dossier contenant les images

    private List<(Texture2D, DateTime)> textureList = new List<(Texture2D, DateTime)>(); // Liste des textures chargées avec leur date d'ajout
    private GameObject[] cubes; // Tableau des cubes

    void Start()
    {
        // Récupérer tous les objets avec le tag "Cube" au démarrage
        cubes = GameObject.FindGameObjectsWithTag("Cube");
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

            // Afficher le nom de la nouvelle image
            Debug.Log("Nouvelle image détectée : " + Path.GetFileName(imagePath));

            // Si le nombre de textures dépasse le nombre de cubes, retirer la texture la plus ancienne
            if (textureList.Count > cubes.Length)
            {
                RemoveOldestTexture();
            }
        }

        // Mettre à jour les textures des cubes
        for (int i = 0; i < cubes.Length; i++)
        {
            // S'assurer qu'il y a une texture associée au cube
            if (i < textureList.Count)
            {
                // Récupérer le renderer du cube
                Renderer cubeRenderer = cubes[i].GetComponent<Renderer>();

                // Assigner la texture au cube
                cubeRenderer.material.mainTexture = textureList[i].Item1;
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

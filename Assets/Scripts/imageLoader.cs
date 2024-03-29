using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class ImageLoader : MonoBehaviour
{
    public string imageFolderPath; // Chemin du dossier contenant les images
    private int maxTextures; // Nombre maximal de textures autorisées

    private List<(Texture2D, DateTime, string)> textureList = new List<(Texture2D, DateTime, string)>(); // Liste des textures chargées avec leur date d'ajout et chemin du fichier
    private GameObject[] templateFaces; // Tableau de template pour mettre les faces
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        // Récupérer tous les objets avec le tag "TemplateFace" au démarrage
        templateFaces = GameObject.FindGameObjectsWithTag("TemplateFaces");
        maxTextures = templateFaces.Length;
        // Lancer la coroutine pour charger les images de manière asynchrone
        StartCoroutine(LoadImagesCoroutine());
    }

    async Task LoadImagesAsync()
    {
        // Vérifier s'il y a de nouvelles images
        string[] currentImages = Directory.GetFiles(imageFolderPath, "*.png");

        foreach (string imagePath in currentImages)
        {
            // Vérifier si l'image existe déjà dans la liste
            if (textureList.Exists(item => item.Item2 == File.GetLastWriteTime(imagePath)))
            {
                continue; // Si elle existe, passer à l'image suivante
            }

            // Charger la nouvelle image depuis le fichier
            byte[] fileData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            // Ajouter la texture à la liste avec sa date d'ajout et le chemin du fichier
            
            OnNewImageDetected(texture);
            // Si le nombre de textures dépasse la limite globale, supprimer le fichier associé
            if (textureList.Count >= maxTextures)
            {
                RemoveOldestTextureAndFile(imagePath);
                
            }
            else{
                textureList.Add((texture, File.GetLastWriteTime(imagePath), imagePath));
            }
            // Attendre un frame avant de charger la prochaine image
            await Task.Delay(1);
        }
    }

    IEnumerator LoadImagesCoroutine()
    {
        while (true)
        {
            // Charger les images de manière asynchrone
            yield return LoadImagesAsync();

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

            // Attendre un frame avant de vérifier à nouveau les nouvelles images
            yield return null;
        }
    }

    void RemoveOldestTextureAndFile(string newFilePath)
    {
        
        // Recherche de l'index de la texture la plus ancienne dans la liste
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

        // Remplacer la texture la plus ancienne par la nouvelle
        if (indexToRemove >= 0)
        {
            // Supprimer l'ancienne texture de la mémoire
            Texture2D oldTexture = textureList[indexToRemove].Item1;
            Destroy(oldTexture);

            // Supprimer l'ancien fichier sur le disque
            string filePathToRemove = textureList[indexToRemove].Item3;
            File.Delete(filePathToRemove);

            // Remplacer par la nouvelle texture
            Texture2D newTexture = new Texture2D(2, 2);
            byte[] fileData = File.ReadAllBytes(newFilePath);
            newTexture.LoadImage(fileData);

            textureList[indexToRemove] = (newTexture, File.GetLastWriteTime(newFilePath), newFilePath);
        }
        
    }


    void OnNewImageDetected(Texture2D newTexture)
    {
    // Vous pouvez appeler d'autres fonctions ou effectuer des actions spécifiques ici
    gameManager.PlaySfxPhoto();
    }

}
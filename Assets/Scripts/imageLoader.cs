/*using UnityEngine;
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
}*/
/*using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

    async void Update()
    {
        // Vérifier s'il y a de nouvelles images à chaque mise à jour de manière asynchrone
        await Task.Run(() => LoadNewImages());

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

    void LoadNewImages()
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

            // Ajouter la texture à la liste avec sa date d'ajout
            textureList.Add((texture, File.GetLastWriteTime(imagePath)));

            // Si le nombre de textures dépasse le nombre de templateFaces, retirer la texture la plus ancienne
            if (textureList.Count > templateFaces.Length)
            {
                RemoveOldestTexture();
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
}*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class ImageLoader : MonoBehaviour
{
    public string imageFolderPath; // Chemin du dossier contenant les images

    private List<(Texture2D, DateTime)> textureList = new List<(Texture2D, DateTime)>(); // Liste des textures chargées avec leur date d'ajout
    private GameObject[] templateFaces; // Tableau de template pour mettre les faces

    void Start()
    {
        // Récupérer tous les objets avec le tag "TemplateFace" au démarrage
        templateFaces = GameObject.FindGameObjectsWithTag("TemplateFaces");

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

            // Ajouter la texture à la liste avec sa date d'ajout
            textureList.Add((texture, File.GetLastWriteTime(imagePath)));

            // Si le nombre de textures dépasse le nombre de templateFaces, retirer la texture la plus ancienne
            if (textureList.Count > templateFaces.Length)
            {
                RemoveOldestTexture();
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





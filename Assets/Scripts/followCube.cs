using UnityEngine;

public class HorseDistribution : MonoBehaviour
{
    public GameObject centerObject; // Objet autour duquel les chevaux doivent tourner
    public float radius = 5f; // Rayon de distribution des chevaux
    public float rotationSpeed = 1f; // Vitesse de rotation des chevaux
    public float headOffset = 1f; // Décalage de la tête par rapport au corps

    private GameObject[] horses; // Tableau pour stocker les chevaux

    void Start()
    {
        FindAndDistributeHorses(); // Appel de la méthode pour trouver et distribuer les chevaux
    }

    void FindAndDistributeHorses()
    {
        horses = GameObject.FindGameObjectsWithTag("Horse"); // Trouver tous les chevaux avec le tag "Horse"
        int numHorses = horses.Length;
        float angleIncrement = 360f / numHorses;
        for (int i = 0; i < numHorses; i++)
        {
            float angle = i * angleIncrement; // Calcul de l'angle pour chaque cheval
            Vector3 offset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius; // Calcul de la position du cheval autour de l'objet central
            horses[i].transform.position = centerObject.transform.position + offset; // Positionnement du cheval

            // Calcul de l'angle pour orienter la tête du cheval
            float headAngle = (i - 1) * angleIncrement; // L'angle de la tête est basé sur l'angle du cheval précédent
            Vector3 headOffsetVector = Quaternion.Euler(0f, headAngle, 0f) * Vector3.forward * headOffset; // Calcul du décalage de la tête
            horses[i].transform.GetChild(0).position = horses[i].transform.position + headOffsetVector; // Positionnement de la tête
        }
    }

    void Update()
    {
        RotateHorses(); // Appel de la méthode pour faire tourner les chevaux
    }

    void RotateHorses()
    {
        int numHorses = horses.Length;
        for (int i = 0; i < numHorses; i++)
        {
            horses[i].transform.RotateAround(centerObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime); // Rotation des chevaux autour de l'objet central
        }
    }
}

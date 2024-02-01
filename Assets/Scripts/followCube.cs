using UnityEngine;

public class CubeDistribution : MonoBehaviour
{
    public GameObject centerObject; // Objet autour duquel les cubes doivent tourner
    public float radius = 5f; // Rayon de distribution des cubes
    public float rotationSpeed = 1f; // Vitesse de rotation des cubes

    private GameObject[] cubes; // Tableau pour stocker les cubes

    void Start()
    {
        FindAndDistributeCubes();
    }

    void FindAndDistributeCubes()
    {
        cubes = GameObject.FindGameObjectsWithTag("Cube"); // Trouver tous les cubes avec le tag "Cube"
        int numCubes = cubes.Length;
        float angleIncrement = 360f / numCubes;
        for (int i = 0; i < numCubes; i++)
        {
            float angle = i * angleIncrement;
            Vector3 offset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
            cubes[i].transform.position = centerObject.transform.position + offset;
        }
    }

    void Update()
    {
        RotateCubes();
    }

    void RotateCubes()
    {
        int numCubes = cubes.Length;
        for (int i = 0; i < numCubes; i++)
        {
            cubes[i].transform.RotateAround(centerObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}

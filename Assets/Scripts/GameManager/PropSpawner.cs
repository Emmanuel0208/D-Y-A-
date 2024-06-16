using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;  // Puntos de spawn para los props
    public GameObject[] propPrefabs;  // Array de prefabs de props

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Seleccionar un prop aleatorio
            int randomIndex = Random.Range(0, propPrefabs.Length);
            GameObject selectedProp = propPrefabs[randomIndex];

            // Crear una instancia del prop
            GameObject newProp = Instantiate(selectedProp, spawnPoint.position, spawnPoint.rotation);

            // Si es el corazón (LifeProp), rotarlo en 90 grados en el eje X
            if (newProp.CompareTag("LifeProp"))
            {
                newProp.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }
}

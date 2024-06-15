using UnityEngine;
using System.Collections.Generic;

public class PropSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;  // Puntos de spawn para los props
    public GameObject[] propPrefabs;  // Prefabs de los props (vida, munici�n, velocidad)

    void Start()
    {
        SpawnProps();
    }

    void SpawnProps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, propPrefabs.Length);
            Instantiate(propPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        }
    }
}

using UnityEngine;
using System.Collections;

public class RaycastEnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;  // Array de puntos de spawn para los raycast enemies
    public GameObject raycastEnemyPrefab;  // Prefab del raycast enemy

    void Start()
    {
        SpawnEnemies();  // Spawnear enemigos al inicio
    }

    // Función para spawnear un raycast enemy en cada punto de spawn
    public void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnRaycastEnemy(spawnPoint);
        }
    }

    // Función para spawnear un raycast enemy en un punto específico
    private void SpawnRaycastEnemy(Transform spawnPoint)
    {
        GameObject enemy = Instantiate(raycastEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        RaycastEnemy enemyScript = enemy.GetComponent<RaycastEnemy>();
        enemyScript.spawnPoint = spawnPoint;  // Asignar el punto de spawn al enemigo
        Debug.Log("Raycast enemy spawneado en " + spawnPoint.name);
    }

    // Función para respawnear un raycast enemy con un retraso de 5 segundos
    public void RespawnEnemyWithDelay(Transform spawnPoint)
    {
        StartCoroutine(RespawnAfterDelay(spawnPoint));
    }

    // Coroutine para manejar el retraso de respawn
    private IEnumerator RespawnAfterDelay(Transform spawnPoint)
    {
        yield return new WaitForSeconds(5f);  // Esperar 5 segundos
        SpawnRaycastEnemy(spawnPoint);
        Debug.Log("Raycast enemy respawneado en " + spawnPoint.name);
    }
}

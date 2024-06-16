using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyDijkstraPrefab;
    public GameObject enemyAStarPrefab;
    public PlayerAttack playerAttack;  // Referencia al script PlayerAttack

    void Start()
    {
        // Opcional: Obtener la referencia al PlayerAttack automáticamente
        if (playerAttack == null)
        {
            playerAttack = FindObjectOfType<PlayerAttack>();
        }
    }

    // Función para spawnear enemigo Dijkstra
    public void SpawnDijkstra()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyDijkstraPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemigo Dijkstra spawneado.");

        // Habilitar la capacidad de disparar en el Player
        if (playerAttack != null)
        {
            playerAttack.canShoot = true;
        }
    }

    // Función para spawnear enemigo A*
    public void SpawnAStar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyAStarPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemigo A* spawneado.");

        // Habilitar la capacidad de disparar en el Player
        if (playerAttack != null)
        {
            playerAttack.canShoot = true;
        }
    }

    // Función para respawnear un enemigo con un retraso de 5 segundos
    public void RespawnEnemy(string enemyType)
    {
        StartCoroutine(RespawnAfterDelay(enemyType));
    }

    // Coroutine para manejar el retraso de respawn
    private IEnumerator RespawnAfterDelay(string enemyType)
    {
        yield return new WaitForSeconds(5f);  // Esperar 5 segundos

        if (enemyType == "Dijkstra")
        {
            SpawnDijkstra();
        }
        else if (enemyType == "AStar")
        {
            SpawnAStar();
        }
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyDijkstraPrefab;
    public GameObject enemyAStarPrefab;

    public void SpawnDijkstra()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyDijkstraPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemigo Dijkstra spawneado.");
    }

    public void SpawnAStar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyAStarPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Enemigo A* spawneado.");
    }
}

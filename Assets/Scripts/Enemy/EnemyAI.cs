using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;  // Referencia al objeto Player
    private GraphManager graphManager;  // Referencia al GraphManager
    public float speed = 3.0f;  // Velocidad constante del Enemy
    public float recalculateInterval = 1.0f;  // Intervalo en segundos para recalcular la ruta
    private List<Node> path;  // Lista que contiene la ruta calculada
    private int currentPathIndex = 0;  // Índice del nodo actual en la ruta
    private Node playerTempNode;  // Nodo temporal que representa la posición del Player
    private Node playerClosestNode;  // Nodo más cercano al Player
    private float timeSinceLastRecalculation;  // Tiempo transcurrido desde la última recalculación
    private Node lastTargetNode;  // Último nodo objetivo para evitar recalculaciones innecesarias

    private EnemySpawner enemySpawner;  // Referencia al spawner de enemigos

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Buscar el Player por etiqueta
        GameObject graphManagerObject = GameObject.FindGameObjectWithTag("GraphManager");  // Buscar el GraphManager por etiqueta

        if (player == null || graphManagerObject == null)
        {
            Debug.LogWarning("Player o GraphManager no encontrado.");
            return;
        }

        graphManager = graphManagerObject.GetComponent<GraphManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();  // Obtener referencia al EnemySpawner

        if (graphManager == null)
        {
            Debug.LogWarning("GraphManager no tiene el componente GraphManager.");
            return;
        }

        playerTempNode = player.GetComponent<DynamicPlayerEdge>().GetTemporaryNode();
        UpdatePath();
    }

    void Update()
    {
        timeSinceLastRecalculation += Time.deltaTime;

        if (timeSinceLastRecalculation >= recalculateInterval)
        {
            UpdatePath();
            timeSinceLastRecalculation = 0;
        }

        MoveAlongPath();
    }

    void UpdatePath()
    {
        playerTempNode = player.GetComponent<DynamicPlayerEdge>().GetTemporaryNode();
        playerClosestNode = player.GetComponent<DynamicPlayerEdge>().GetClosestNode();

        if (playerTempNode == null || playerClosestNode == null) return;

        Node enemyNode = graphManager.FindClosestNode(transform.position);

        List<Node> allNodes = new List<Node>(graphManager.nodes);
        if (!allNodes.Contains(playerTempNode))
        {
            allNodes.Add(playerTempNode);
        }

        List<Node> newPath = graphManager.FindShortestPath(enemyNode, playerTempNode);

        if (newPath.Count > 1 && (lastTargetNode == null || newPath[1] != lastTargetNode))
        {
            path = newPath;
            currentPathIndex = 0;
            lastTargetNode = newPath[1];
        }

        playerClosestNode.AddConnection(playerTempNode);
    }

    void MoveAlongPath()
    {
        if (path == null || path.Count == 0 || currentPathIndex >= path.Count) return;

        Node targetNode = path[currentPathIndex];
        Vector3 targetPosition = targetNode.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir al enemigo y aplicar daño al jugador
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);  // Asegúrate de que el jugador tenga un componente PlayerHealth
            RespawnEnemy();
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            // Destruir al enemigo y al proyectil
            Destroy(gameObject);
            Destroy(collision.gameObject);
            RespawnEnemy();
        }
    }

    public void RespawnEnemy()
    {
        if (enemySpawner != null)
        {
            string enemyType = gameObject.tag == "Dijkstra" ? "Dijkstra" : "AStar";
            enemySpawner.RespawnEnemy(enemyType);
        }
    }

    private void OnDrawGizmos()
    {
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
            }
        }
    }
}

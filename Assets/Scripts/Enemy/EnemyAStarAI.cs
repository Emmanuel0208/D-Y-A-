using UnityEngine;
using System.Collections.Generic;

public class EnemyAStarAI : MonoBehaviour
{
    private GameObject player;  // Referencia al objeto Player
    private GraphManagerAStar graphManager;  // Referencia al GraphManagerAStar
    public float speed = 3.0f;  // Velocidad constante del enemigo
    public float recalculateInterval = 1.0f;  // Intervalo en segundos para recalcular la ruta

    private List<Node> path;  // Lista que contiene la ruta calculada
    private int currentPathIndex = 0;  // Índice del nodo actual en la ruta
    private Node playerTempNode;  // Nodo temporal que representa la posición del jugador
    private Node playerClosestNode;  // Nodo más cercano al jugador
    private float timeSinceLastRecalculation;  // Tiempo transcurrido desde la última recalculación
    private Node lastTargetNode;  // Último nodo objetivo para evitar recalculaciones innecesarias

    private EnemySpawner enemySpawner;  // Referencia al spawner de enemigos

    void Start()
    {
        // Buscar el objeto Player por su etiqueta.
        player = GameObject.FindGameObjectWithTag("Player");
        // Buscar el objeto GraphManager por su etiqueta.
        GameObject graphManagerObject = GameObject.FindGameObjectWithTag("GraphManager");

        // Verificar si se encontraron el Player y el GraphManager.
        if (player == null || graphManagerObject == null)
        {
            Debug.LogWarning("Player o GraphManager no encontrado.");
            return;
        }

        // Obtener el componente GraphManagerAStar del GraphManager.
        graphManager = graphManagerObject.GetComponent<GraphManagerAStar>();
        // Obtener referencia al spawner de enemigos en la escena.
        enemySpawner = FindObjectOfType<EnemySpawner>();

        // Verificar si el GraphManager tiene el componente GraphManagerAStar.
        if (graphManager == null)
        {
            Debug.LogWarning("GraphManager no tiene el componente GraphManagerAStar.");
            return;
        }

        // Obtener el nodo temporal que representa la posición del jugador.
        playerTempNode = player.GetComponent<DynamicPlayerEdge>().GetTemporaryNode();
        // Actualizar la ruta al inicio.
        UpdatePath();
    }

    void Update()
    {
        // Incrementar el tiempo transcurrido desde la última recalculación.
        timeSinceLastRecalculation += Time.deltaTime;

        // Recalcular la ruta si ha pasado el intervalo especificado.
        if (timeSinceLastRecalculation >= recalculateInterval)
        {
            UpdatePath();
            timeSinceLastRecalculation = 0;
        }

        // Mover al enemigo a lo largo de la ruta calculada.
        MoveAlongPath();
    }

    void UpdatePath()
    {
        // Actualizar el nodo temporal y el nodo más cercano al jugador.
        playerTempNode = player.GetComponent<DynamicPlayerEdge>().GetTemporaryNode();
        playerClosestNode = player.GetComponent<DynamicPlayerEdge>().GetClosestNode();

        // Verificar si se obtuvieron los nodos correctamente.
        if (playerTempNode == null || playerClosestNode == null) return;

        // Encontrar el nodo más cercano al enemigo en la gráfica.
        Node enemyNode = graphManager.FindClosestNode(transform.position);

        // Crear una lista de todos los nodos y agregar el nodo temporal si no está presente.
        List<Node> allNodes = new List<Node>(graphManager.nodes);
        if (!allNodes.Contains(playerTempNode))
        {
            allNodes.Add(playerTempNode);
        }

        // Calcular una nueva ruta desde el nodo del enemigo al nodo temporal del jugador usando A*.
        List<Node> newPath = graphManager.FindPathAStar(enemyNode, playerTempNode);

        // Actualizar la ruta si es válida y diferente de la anterior.
        if (newPath.Count > 1 && (lastTargetNode == null || newPath[1] != lastTargetNode))
        {
            path = newPath;
            currentPathIndex = 0;
            lastTargetNode = newPath[1];
        }

        // Conectar el nodo más cercano al jugador con el nodo temporal.
        playerClosestNode.AddConnection(playerTempNode);
    }

    void MoveAlongPath()
    {
        // Verificar si hay una ruta válida y si el enemigo no ha alcanzado el final de la ruta.
        if (path == null || path.Count == 0 || currentPathIndex >= path.Count) return;

        // Obtener el nodo objetivo actual de la ruta.
        Node targetNode = path[currentPathIndex];
        Vector3 targetPosition = targetNode.transform.position;

        // Mover al enemigo hacia la posición del nodo objetivo.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotar al enemigo para que mire hacia el jugador.
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        // Pasar al siguiente nodo si el enemigo está suficientemente cerca del nodo objetivo.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el enemigo colisiona con el jugador.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir al enemigo y aplicar daño al jugador.
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);  // Asegúrate de que el jugador tenga un componente PlayerHealth
            RespawnEnemy();
            Debug.Log("Player encontrado");
        }
        // Verificar si el enemigo colisiona con un proyectil.
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            // Destruir al enemigo y al proyectil.
            Destroy(gameObject);
            Destroy(collision.gameObject);
            RespawnEnemy();
        }
    }

    public void RespawnEnemy()
    {
        // Verificar si hay un EnemySpawner en la escena.
        if (enemySpawner != null)
        {
            // Determinar el tipo de enemigo basado en su etiqueta y solicitar un respawn.
            string enemyType = gameObject.tag == "Dijkstra" ? "Dijkstra" : "AStar";
            enemySpawner.RespawnEnemy(enemyType);
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar la ruta en la vista de escena para facilitar la depuración.
        if (path != null && path.Count > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class EnemyAStarAI : MonoBehaviour
{
    public GameObject player;                 // Referencia al objeto Player
    public GraphManagerAStar graphManager;    // Referencia al GraphManagerAStar
    public float speed = 3.0f;                // Velocidad constante del Enemy
    public float recalculateInterval = 1.0f;  // Intervalo en segundos para recalcular la ruta
    private List<Node> path;                  // Lista que contiene la ruta calculada
    private int currentPathIndex = 0;         // Índice del nodo actual en la ruta
    private Node playerTempNode;              // Nodo temporal que representa la posición del Player
    private Node playerClosestNode;           // Nodo más cercano al Player
    private float timeSinceLastRecalculation; // Tiempo transcurrido desde la última recalculación
    private Node lastTargetNode;              // Último nodo objetivo para evitar recalculaciones innecesarias

    void Start()
    {
        if (player == null || graphManager == null)
        {
            Debug.LogWarning("Player or GraphManager not set.");
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

        // Añadir el nodo temporal a la lista de nodos del grafo para el cálculo de la ruta
        List<Node> allNodes = new List<Node>(graphManager.nodes);
        if (!allNodes.Contains(playerTempNode))
        {
            allNodes.Add(playerTempNode);
        }

        // Calcular la ruta más corta usando A*
        List<Node> newPath = graphManager.FindPathAStar(enemyNode, playerTempNode);

        // Verificar si la ruta calculada es diferente de la anterior para evitar atascos
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

        // Hacer que el Enemy mire hacia el Player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex++;
        }
    }

    private void OnDrawGizmos()
    {
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

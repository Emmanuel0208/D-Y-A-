using UnityEngine;

public class DynamicPlayerEdge : MonoBehaviour
{
    // Referencia al gestor del grafo.
    public GraphManager graphManager;
    // Array de todos los nodos en el grafo.
    public Node[] allNodes;
    // Nodo más cercano al jugador.
    private Node closestNodeToPlayer;
    // Nodo temporal que representa la posición actual del jugador.
    private Node tempNode;

    // Método Start se llama una vez al inicio.
    void Start()
    {
        // Verificar si el GraphManager está asignado.
        if (graphManager == null)
        {
            Debug.LogError("GraphManager not set in DynamicPlayerEdge. Please assign it in the Inspector.");
            return;
        }

        // Verificar si hay nodos asignados.
        if (allNodes == null || allNodes.Length == 0)
        {
            Debug.LogError("No nodes available in DynamicPlayerEdge. Please assign them in the Inspector.");
            return;
        }

        // Crear un nuevo GameObject para el nodo temporal.
        GameObject tempNodeObject = new GameObject("PlayerTemporaryNode");
        // Añadir el componente Node al objeto y guardarlo en tempNode.
        tempNode = tempNodeObject.AddComponent<Node>();
        // Establecer la posición inicial del nodo temporal a la posición actual del jugador.
        tempNodeObject.transform.position = transform.position;
    }

    // Método Update se llama una vez por frame.
    void Update()
    {
        // Verificar si hay nodos asignados.
        if (allNodes == null || allNodes.Length == 0)
        {
            Debug.LogError("No nodes assigned to allNodes in DynamicPlayerEdge.");
            return;
        }

        // Actualizar el nodo más cercano al jugador.
        UpdateClosestNode();
    }

    // Método para actualizar el nodo más cercano al jugador.
    void UpdateClosestNode()
    {
        // Verificar si el GraphManager está asignado.
        if (graphManager == null)
        {
            Debug.LogError("GraphManager is not assigned in DynamicPlayerEdge.");
            return;
        }

        // Verificar si el nodo temporal está inicializado.
        if (tempNode == null)
        {
            Debug.LogError("Temporary node not initialized in DynamicPlayerEdge.");
            return;
        }

        // Si ya hay un nodo más cercano asignado, eliminar la conexión anterior.
        if (closestNodeToPlayer != null)
        {
            closestNodeToPlayer.RemoveConnection(tempNode);
        }

        // Encontrar el nodo más cercano a la posición actual del jugador.
        closestNodeToPlayer = graphManager.FindClosestNode(transform.position);

        // Si se encuentra un nodo más cercano, actualizar la posición del nodo temporal y añadir la conexión.
        if (closestNodeToPlayer != null)
        {
            tempNode.transform.position = transform.position;
            closestNodeToPlayer.AddConnection(tempNode);
        }
    }

    // Método para obtener el nodo temporal.
    public Node GetTemporaryNode()
    {
        return tempNode;
    }

    // Método para obtener el nodo más cercano al jugador.
    public Node GetClosestNode()
    {
        return closestNodeToPlayer;
    }
}

using UnityEngine;

public class DynamicPlayerEdge : MonoBehaviour
{
    // Referencia al gestor del grafo.
    public GraphManager graphManager;
    // Array de todos los nodos en el grafo.
    public Node[] allNodes;
    // Nodo m�s cercano al jugador.
    private Node closestNodeToPlayer;
    // Nodo temporal que representa la posici�n actual del jugador.
    private Node tempNode;

    // M�todo Start se llama una vez al inicio.
    void Start()
    {
        // Verificar si el GraphManager est� asignado.
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
        // A�adir el componente Node al objeto y guardarlo en tempNode.
        tempNode = tempNodeObject.AddComponent<Node>();
        // Establecer la posici�n inicial del nodo temporal a la posici�n actual del jugador.
        tempNodeObject.transform.position = transform.position;
    }

    // M�todo Update se llama una vez por frame.
    void Update()
    {
        // Verificar si hay nodos asignados.
        if (allNodes == null || allNodes.Length == 0)
        {
            Debug.LogError("No nodes assigned to allNodes in DynamicPlayerEdge.");
            return;
        }

        // Actualizar el nodo m�s cercano al jugador.
        UpdateClosestNode();
    }

    // M�todo para actualizar el nodo m�s cercano al jugador.
    void UpdateClosestNode()
    {
        // Verificar si el GraphManager est� asignado.
        if (graphManager == null)
        {
            Debug.LogError("GraphManager is not assigned in DynamicPlayerEdge.");
            return;
        }

        // Verificar si el nodo temporal est� inicializado.
        if (tempNode == null)
        {
            Debug.LogError("Temporary node not initialized in DynamicPlayerEdge.");
            return;
        }

        // Si ya hay un nodo m�s cercano asignado, eliminar la conexi�n anterior.
        if (closestNodeToPlayer != null)
        {
            closestNodeToPlayer.RemoveConnection(tempNode);
        }

        // Encontrar el nodo m�s cercano a la posici�n actual del jugador.
        closestNodeToPlayer = graphManager.FindClosestNode(transform.position);

        // Si se encuentra un nodo m�s cercano, actualizar la posici�n del nodo temporal y a�adir la conexi�n.
        if (closestNodeToPlayer != null)
        {
            tempNode.transform.position = transform.position;
            closestNodeToPlayer.AddConnection(tempNode);
        }
    }

    // M�todo para obtener el nodo temporal.
    public Node GetTemporaryNode()
    {
        return tempNode;
    }

    // M�todo para obtener el nodo m�s cercano al jugador.
    public Node GetClosestNode()
    {
        return closestNodeToPlayer;
    }
}

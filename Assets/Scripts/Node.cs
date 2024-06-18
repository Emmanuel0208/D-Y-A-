using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    // Lista que almacena los nodos conectados a este nodo.
    public List<Node> connectedNodes = new List<Node>();

    // Método para agregar una conexión a otro nodo.
    public void AddConnection(Node node)
    {
        // Verifica si el nodo no está ya en la lista de conexiones.
        if (!connectedNodes.Contains(node))
        {
            // Agrega el nodo a la lista de conexiones.
            connectedNodes.Add(node);
            // Llama al método AddConnection del otro nodo para hacer la conexión bidireccional.
            node.AddConnection(this);
        }
    }

    // Método para eliminar una conexión a otro nodo.
    public void RemoveConnection(Node node)
    {
        // Verifica si el nodo está en la lista de conexiones.
        if (connectedNodes.Contains(node))
        {
            // Elimina el nodo de la lista de conexiones.
            connectedNodes.Remove(node);
            // Llama al método RemoveConnection del otro nodo para eliminar la conexión bidireccional.
            node.RemoveConnection(this);
        }
    }

    // Método de Unity que se llama para dibujar Gizmos en la vista de escena.
    private void OnDrawGizmos()
    {
        // Establece el color del Gizmo a verde.
        Gizmos.color = Color.green;
        // Recorre cada nodo conectado.
        foreach (Node node in connectedNodes)
        {
            // Verifica si el nodo no es nulo.
            if (node != null)
            {
                // Dibuja una línea entre la posición de este nodo y el nodo conectado.
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}

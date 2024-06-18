using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    // Lista que almacena los nodos conectados a este nodo.
    public List<Node> connectedNodes = new List<Node>();

    // M�todo para agregar una conexi�n a otro nodo.
    public void AddConnection(Node node)
    {
        // Verifica si el nodo no est� ya en la lista de conexiones.
        if (!connectedNodes.Contains(node))
        {
            // Agrega el nodo a la lista de conexiones.
            connectedNodes.Add(node);
            // Llama al m�todo AddConnection del otro nodo para hacer la conexi�n bidireccional.
            node.AddConnection(this);
        }
    }

    // M�todo para eliminar una conexi�n a otro nodo.
    public void RemoveConnection(Node node)
    {
        // Verifica si el nodo est� en la lista de conexiones.
        if (connectedNodes.Contains(node))
        {
            // Elimina el nodo de la lista de conexiones.
            connectedNodes.Remove(node);
            // Llama al m�todo RemoveConnection del otro nodo para eliminar la conexi�n bidireccional.
            node.RemoveConnection(this);
        }
    }

    // M�todo de Unity que se llama para dibujar Gizmos en la vista de escena.
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
                // Dibuja una l�nea entre la posici�n de este nodo y el nodo conectado.
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}

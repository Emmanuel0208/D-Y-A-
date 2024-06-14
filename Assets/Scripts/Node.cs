using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes = new List<Node>();

    public void AddConnection(Node node)
    {
        if (!connectedNodes.Contains(node))
        {
            connectedNodes.Add(node);
            node.AddConnection(this); // Conexi�n bidireccional
        }
    }

    public void RemoveConnection(Node node)
    {
        if (connectedNodes.Contains(node))
        {
            connectedNodes.Remove(node);
            node.RemoveConnection(this); // Eliminar la conexi�n bidireccional
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        foreach (Node node in connectedNodes)
        {
            if (node != null)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}

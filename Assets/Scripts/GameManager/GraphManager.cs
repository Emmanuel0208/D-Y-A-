using UnityEngine;
using System.Collections.Generic;

public class GraphManager : MonoBehaviour
{
    public Node[] nodes;

    public List<Node> FindShortestPath(Node startNode, Node endNode)
    {
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();
        List<Node> unvisitedNodes = new List<Node>(nodes);

        // Añadir el nodo temporal si no está ya en la lista
        if (!unvisitedNodes.Contains(endNode))
        {
            unvisitedNodes.Add(endNode);
        }

        List<Node> path = new List<Node>();

        foreach (Node node in unvisitedNodes)
        {
            distances[node] = float.MaxValue;
            previousNodes[node] = null;
        }
        distances[startNode] = 0;

        while (unvisitedNodes.Count > 0)
        {
            unvisitedNodes.Sort((node1, node2) => distances[node1].CompareTo(distances[node2]));
            Node currentNode = unvisitedNodes[0];
            unvisitedNodes.Remove(currentNode);

            if (currentNode == endNode)
            {
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = previousNodes[currentNode];
                }
                break;
            }

            foreach (Node neighbor in currentNode.connectedNodes)
            {
                float newDist = distances[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);
                if (newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    previousNodes[neighbor] = currentNode;
                }
            }
        }

        return path;
    }

    public Node FindClosestNode(Vector3 position)
    {
        Node closestNode = null;
        float shortestDistance = float.MaxValue;

        foreach (Node node in nodes)
        {
            float distance = Vector3.Distance(node.transform.position, position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }
}

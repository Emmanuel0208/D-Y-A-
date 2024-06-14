using UnityEngine;
using System.Collections.Generic;

public class GraphManagerAStar : MonoBehaviour
{
    public Node[] nodes;

    public List<Node> FindPathAStar(Node startNode, Node endNode)
    {
        Dictionary<Node, float> gCosts = new Dictionary<Node, float>();
        Dictionary<Node, float> fCosts = new Dictionary<Node, float>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        List<Node> path = new List<Node>();

        foreach (Node node in nodes)
        {
            gCosts[node] = float.MaxValue;
            fCosts[node] = float.MaxValue;
        }
        gCosts[startNode] = 0;
        fCosts[startNode] = HeuristicCostEstimate(startNode, endNode);

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            openSet.Sort((node1, node2) => fCosts[node1].CompareTo(fCosts[node2]));
            Node currentNode = openSet[0];

            if (currentNode == endNode)
            {
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = cameFrom.ContainsKey(currentNode) ? cameFrom[currentNode] : null;
                }
                break;
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (Node neighbor in currentNode.connectedNodes)
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeGCost = gCosts[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGCost >= gCosts[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = currentNode;
                gCosts[neighbor] = tentativeGCost;
                fCosts[neighbor] = gCosts[neighbor] + HeuristicCostEstimate(neighbor, endNode);
            }
        }

        return path;
    }

    private float HeuristicCostEstimate(Node node, Node endNode)
    {
        // Usamos la distancia Euclidiana como heurística
        return Vector3.Distance(node.transform.position, endNode.transform.position);
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

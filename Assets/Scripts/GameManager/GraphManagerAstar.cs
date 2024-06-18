using UnityEngine;
using System.Collections.Generic;

public class GraphManagerAStar : MonoBehaviour
{
    // Array de nodos que forman parte del grafo.
    public Node[] nodes;

    // M�todo para encontrar la ruta m�s corta desde startNode hasta endNode utilizando el algoritmo A*.
    public List<Node> FindPathAStar(Node startNode, Node endNode)
    {
        // Diccionario para almacenar los costos g (distancia desde el nodo de inicio a cada nodo).
        Dictionary<Node, float> gCosts = new Dictionary<Node, float>();
        // Diccionario para almacenar los costos f (estimaci�n del costo total desde el inicio hasta el objetivo).
        Dictionary<Node, float> fCosts = new Dictionary<Node, float>();
        // Diccionario para almacenar de d�nde venimos para cada nodo (utilizado para reconstruir la ruta).
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        // Lista de nodos que se deben evaluar.
        List<Node> openSet = new List<Node>();
        // Conjunto de nodos que ya se han evaluado.
        HashSet<Node> closedSet = new HashSet<Node>();
        // Lista para almacenar la ruta m�s corta encontrada.
        List<Node> path = new List<Node>();

        // Inicializar los costos g y f para cada nodo a infinito.
        foreach (Node node in nodes)
        {
            gCosts[node] = float.MaxValue;
            fCosts[node] = float.MaxValue;
        }
        // El costo g del nodo de inicio es 0.
        gCosts[startNode] = 0;
        // El costo f del nodo de inicio es la estimaci�n heur�stica hasta el nodo final.
        fCosts[startNode] = HeuristicCostEstimate(startNode, endNode);

        // A�adir el nodo de inicio al conjunto abierto.
        openSet.Add(startNode);

        // Bucle principal para evaluar nodos.
        while (openSet.Count > 0)
        {
            // Ordenar el conjunto abierto por el costo f.
            openSet.Sort((node1, node2) => fCosts[node1].CompareTo(fCosts[node2]));
            // Seleccionar el nodo con el costo f m�s bajo.
            Node currentNode = openSet[0];

            // Si hemos llegado al nodo final, reconstruimos la ruta.
            if (currentNode == endNode)
            {
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = cameFrom.ContainsKey(currentNode) ? cameFrom[currentNode] : null;
                }
                break;
            }

            // Mover el nodo actual del conjunto abierto al cerrado.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // Evaluar todos los nodos vecinos.
            foreach (Node neighbor in currentNode.connectedNodes)
            {
                // Si el vecino ya est� en el conjunto cerrado, lo ignoramos.
                if (closedSet.Contains(neighbor)) continue;

                // Calcular el costo g tentativo para el vecino.
                float tentativeGCost = gCosts[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);

                // Si el vecino no est� en el conjunto abierto, lo a�adimos.
                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                // Si el nuevo costo g no es mejor que el costo g conocido, ignoramos este vecino.
                else if (tentativeGCost >= gCosts[neighbor])
                {
                    continue;
                }

                // Actualizar la mejor ruta conocida hacia este vecino.
                cameFrom[neighbor] = currentNode;
                gCosts[neighbor] = tentativeGCost;
                // Actualizar el costo f para el vecino.
                fCosts[neighbor] = gCosts[neighbor] + HeuristicCostEstimate(neighbor, endNode);
            }
        }

        return path;
    }

    // M�todo para estimar el costo desde un nodo hasta el nodo final utilizando una heur�stica (distancia Euclidiana).
    private float HeuristicCostEstimate(Node node, Node endNode)
    {
        return Vector3.Distance(node.transform.position, endNode.transform.position);
    }

    // M�todo para encontrar el nodo m�s cercano a una posici�n dada.
    public Node FindClosestNode(Vector3 position)
    {
        Node closestNode = null;
        float shortestDistance = float.MaxValue;

        // Iterar sobre todos los nodos para encontrar el m�s cercano.
        foreach (Node node in nodes)
        {
            float distance = Vector3.Distance(node.transform.position, position);
            // Actualizar el nodo m�s cercano si se encuentra uno m�s pr�ximo.
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }
}

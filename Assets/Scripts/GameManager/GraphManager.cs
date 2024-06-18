using UnityEngine;
using System.Collections.Generic;

public class GraphManager : MonoBehaviour
{
    // Array de nodos que forman parte del grafo.
    public Node[] nodes;

    // M�todo para encontrar la ruta m�s corta desde startNode hasta endNode.
    public List<Node> FindShortestPath(Node startNode, Node endNode)
    {
        // Diccionario para almacenar las distancias desde el nodo de inicio a cada nodo.
        Dictionary<Node, float> distances = new Dictionary<Node, float>();
        // Diccionario para almacenar el nodo anterior en la ruta �ptima hacia cada nodo.
        Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();
        // Lista de nodos que a�n no han sido visitados.
        List<Node> unvisitedNodes = new List<Node>(nodes);

        // A�adir el nodo final a la lista si no est� ya presente.
        if (!unvisitedNodes.Contains(endNode))
        {
            unvisitedNodes.Add(endNode);
        }

        // Lista para almacenar la ruta m�s corta encontrada.
        List<Node> path = new List<Node>();

        // Inicializar todas las distancias como infinito y los nodos anteriores como nulos.
        foreach (Node node in unvisitedNodes)
        {
            distances[node] = float.MaxValue;
            previousNodes[node] = null;
        }
        // La distancia desde el nodo de inicio a s� mismo es 0.
        distances[startNode] = 0;

        // Algoritmo principal para encontrar la ruta m�s corta.
        while (unvisitedNodes.Count > 0)
        {
            // Ordenar los nodos no visitados por distancia.
            unvisitedNodes.Sort((node1, node2) => distances[node1].CompareTo(distances[node2]));
            // Seleccionar el nodo con la distancia m�nima.
            Node currentNode = unvisitedNodes[0];
            // Remover el nodo actual de la lista de no visitados.
            unvisitedNodes.Remove(currentNode);

            // Si se ha llegado al nodo final, reconstruir la ruta.
            if (currentNode == endNode)
            {
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = previousNodes[currentNode];
                }
                break;
            }

            // Actualizar las distancias para los nodos vecinos del nodo actual.
            foreach (Node neighbor in currentNode.connectedNodes)
            {
                float newDist = distances[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);
                // Si se encuentra una ruta m�s corta a un vecino, actualizar su distancia y nodo anterior.
                if (newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    previousNodes[neighbor] = currentNode;
                }
            }
        }

        return path;
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

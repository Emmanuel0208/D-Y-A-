using UnityEngine;

public class DynamicPlayerEdge : MonoBehaviour
{
    public GraphManager graphManager;
    public Node[] allNodes;
    private Node closestNodeToPlayer;
    private Node tempNode;

    void Start()
    {
        if (graphManager == null)
        {
            Debug.LogError("GraphManager not set in DynamicPlayerEdge. Please assign it in the Inspector.");
            return;
        }

        if (allNodes == null || allNodes.Length == 0)
        {
            Debug.LogError("No nodes available in DynamicPlayerEdge. Please assign them in the Inspector.");
            return;
        }

        GameObject tempNodeObject = new GameObject("PlayerTemporaryNode");
        tempNode = tempNodeObject.AddComponent<Node>();
        tempNodeObject.transform.position = transform.position;
    }

    void Update()
    {
        if (allNodes == null || allNodes.Length == 0)
        {
            Debug.LogError("No nodes assigned to allNodes in DynamicPlayerEdge.");
            return;
        }

        UpdateClosestNode();
    }

    void UpdateClosestNode()
    {
        if (graphManager == null)
        {
            Debug.LogError("GraphManager is not assigned in DynamicPlayerEdge.");
            return;
        }

        if (tempNode == null)
        {
            Debug.LogError("Temporary node not initialized in DynamicPlayerEdge.");
            return;
        }

        if (closestNodeToPlayer != null)
        {
            closestNodeToPlayer.RemoveConnection(tempNode);
        }

        closestNodeToPlayer = graphManager.FindClosestNode(transform.position);

        if (closestNodeToPlayer != null)
        {
            tempNode.transform.position = transform.position;
            closestNodeToPlayer.AddConnection(tempNode);
        }
    }

    public Node GetTemporaryNode()
    {
        return tempNode;
    }

    public Node GetClosestNode()
    {
        return closestNodeToPlayer;
    }
}

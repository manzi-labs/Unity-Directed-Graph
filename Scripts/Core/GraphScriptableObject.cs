using System.Collections.Generic;
using Core;
using UnityEngine;

[CreateAssetMenu(menuName = "Graphs/Graph SO")]
public class GraphScriptableObject : ScriptableObject
{
    public List<GraphNode> nodes;
    
    public List<GraphNode> GetNodes()
    {
        return nodes;
    }
        
    public List<GraphEdge> GetEdgesFromNode(GraphNode node)
    {
        return node.edges;
    }
        
    public void AddNode(Vector3 position)
    {
        GraphNode newNode = new GraphNode(position);
        nodes.Add(newNode);
    }

    public void UpdateNode(GraphNode node, Vector3 newPosition)
    {
        node.position = newPosition;
    }
    
    public void RemoveNode(GraphNode node)
    {
        nodes.Remove(node);
    }
    
    public void AddEdge(GraphNode fromNode, GraphNode toNode)
    {
        GraphEdge newEdge = new GraphEdge(nodes.IndexOf(toNode));
        newEdge.controlA = Vector3.Lerp(fromNode.position, toNode.position, 0.5f);
        newEdge.controlB = Vector3.Lerp(fromNode.position, toNode.position, 0.5f);
        fromNode.edges.Add(newEdge);
    }
    
    public void UpdateEdge(GraphNode startNode, int edgeIndex, Vector3 controlA, Vector3 controlB)
    {
        startNode.edges[edgeIndex].controlA = controlA;
        startNode.edges[edgeIndex].controlB = controlB;
    }

    public void RemoveEdge(GraphNode startNode, int edgeIndex)
    {
        startNode.edges.RemoveAt(edgeIndex);
    }
    
    
}
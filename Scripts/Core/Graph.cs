using System.Collections.Generic;
using Editor_Tools;
using UnityEngine;

namespace Core
{
    public class Graph : MonoBehaviour
    {
        public string graphName;
        public List<GraphNode> nodes;
        public GraphScriptableObject graph;

        public void SerializeGraph()
        {
            SaveSystem<List<GraphNode>>.SaveData(graph.nodes, graphName);
        }

        public void DeSerializeGraph()
        {
            nodes = SaveSystem<List<GraphNode>>.LoadData(graphName);
            graph.nodes = nodes;
        }
       
    }
}
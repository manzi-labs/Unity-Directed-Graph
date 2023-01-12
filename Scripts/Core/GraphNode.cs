using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class GraphNode
    {
        public Vector3 position;
        public List<GraphEdge> edges;

        public GraphNode(Vector3 _position)
        {
            this.position = _position;
            this.edges = new List<GraphEdge>();
        }
    }
}
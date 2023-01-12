using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GraphEdge
    {
        public int toNodeIndex;

        public Vector3 controlA;
        public Vector3 controlB;

        public GraphEdge(int _toNodeIndex)
        {
            toNodeIndex = _toNodeIndex;
        }
    }
}
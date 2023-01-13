using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GraphEdge
    {
        public int toNodeIndex;

        public SerializableVector3 controlA;
        public SerializableVector3 controlB;

        public GraphEdge(int _toNodeIndex)
        {
            toNodeIndex = _toNodeIndex;
        }
    }
}
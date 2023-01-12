using System;
using UnityEngine;

namespace Visuals
{
    public class NodeHook : MonoBehaviour
    {
        private GraphAPI api;
        private Color gizColor;
        public float radius = 1;
        
        public bool addingEdge;

        public void Init(GraphAPI graphAPI)
        {
            this.api = graphAPI;
            addingEdge = false;
        }
        
        public void SyncNode()
        {
            api.SyncNode(this);
        }

        public void AddEdge()
        {
            api.AddEdge(this);
            addingEdge = true;
        }

        public void RemoveNode()
        {
            api.DestroyNode(this);
        }

        private void OnDrawGizmos()
        {
            if (addingEdge)
            {
                gizColor = Color.green;
            }
            else
            {
                gizColor = Color.red;
            }

            Gizmos.color = gizColor;
            Gizmos.DrawSphere(this.transform.position, radius);
        }

        private void OnDrawGizmosSelected()
        {
            if (addingEdge)
            {
                gizColor = Color.green;
            }
            else
            {
                gizColor = Color.yellow;
            }
            
            Gizmos.color = gizColor;
            Gizmos.DrawSphere(this.transform.position, radius);

        }
    }
}
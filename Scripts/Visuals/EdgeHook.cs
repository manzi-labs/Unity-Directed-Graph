using System;
using System.Collections.Generic;
using Core;
using Editor_Tools;
using UnityEditor;
using UnityEngine;

namespace Visuals
{
    [ExecuteInEditMode]
    public class EdgeHook : MonoBehaviour
    {
        private GraphAPI api;
        public int edgeIndex;
        public Vector3 controlA;
        public Vector3 controlB;
        public Vector3 startNode;
        public Vector3 endNode;
        public float radius = 0.5f;

        private GameObject cA;
        private GameObject cB;

        public void OnEnable()
        {
            if(cA == null)
            {
                cA = new GameObject();
                cA.transform.parent = this.transform;
                cA.name = "control A";
            }

            if (cB == null)
            {
                cB = new GameObject();
                cB.transform.parent = this.transform;
                cB.name = "control B";
            }

            radius = 0.5f;

        }

        public void Init(GraphAPI gAPI, int _edgeIndex, Vector3 _startNode, Vector3 _endNode, Vector3 _controlA, Vector3 _controlB)
        {
            this.api = gAPI;
            this.edgeIndex = _edgeIndex;
            this.startNode = _startNode;
            this.endNode = _endNode;
            this.controlA = _controlA;
            this.controlB = _controlB;
            this.transform.position = (endNode + startNode) / 2;
            cA.transform.position = controlA;
            cB.transform.position = controlB;
        }

        public void SyncEdge()
        {
            controlA = cA.transform.position;
            controlB = cB.transform.position;
            api.SyncEdge(this);
        }

        public void RemoveEdge()
        {
            api.DestroyEdge(this);
        }
        
        private void OnDrawGizmos()
        {
            controlA = cA.transform.position;
            controlB = cB.transform.position;

            
            List<Vector3> waypoints = BezierUtilities.GetWaypoints(GetCurve(), 30);
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.color = Color.Lerp(Color.green, Color.red, (float)i / waypoints.Count);

                
                Gizmos.DrawSphere(waypoints[i], radius);
            }
            
            Gizmos.color = Color.black;
            Gizmos.DrawLine(startNode, cA.transform.position);
            Gizmos.DrawLine(endNode, cB.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            controlA = cA.transform.position;
            controlB = cB.transform.position;
            
            
            List<Vector3> waypoints = BezierUtilities.GetWaypoints(GetCurve(), 30);
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.color = Color.Lerp(Color.green, Color.red, (float)i / waypoints.Count);

                Gizmos.DrawSphere(waypoints[i], radius);
            }
            
            Gizmos.color = Color.black;
            Gizmos.DrawLine(startNode, cA.transform.position);
            Gizmos.DrawLine(endNode, cB.transform.position);
        }

        private BezierCurve GetCurve()
        {
            BezierCurve curve = new BezierCurve(startNode, controlA, controlB, endNode);

            return curve;
        }

        public void CopyEdgeControlPoints()
        {
            api.CopyEdge(this);
        }
    }
}
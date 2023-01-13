using System;
using System.Collections.Generic;
using Core;
using UnityEditor;
using UnityEngine;

namespace Visuals
{
    [ExecuteInEditMode]
    public class GraphAPI : MonoBehaviour
    {
        private GameObject root;
        public GraphScriptableObject graph;
        public GameObject nodePrefab;
        public GameObject edgePrefab;
        
        private Dictionary<NodeHook, GraphNode> _nodeDictionary;
        private Dictionary<EdgeHook, GraphNode> _edgeDictionary;
        private List<GameObject> _nodeObjects;
        private List<GameObject> _edgeObjects;

        [SerializeField]private NodeHook _startNode;
        [SerializeField]private NodeHook _endNode;

        //Editor Button
        public void AddNode()
        {
            graph.AddNode(this.transform.position);
            UpdateGraphView();
        }
        private void RemoveNode(GraphNode node)
        {
            //remove connecting edges
            foreach (GraphNode graphNode in graph.GetNodes())
            {
                for (int i = graphNode.edges.Count-1; i >= 0; i--)
                {
                    if (graphNode.edges[i].toNodeIndex == graph.GetNodes().IndexOf(node))
                    {
                        graphNode.edges.Remove(graphNode.edges[i]);
                    }
                }
            }
            
            graph.RemoveNode(node);
            
            UpdateGraphView();
        }
        
        
        //Reset scene objects
        public void UpdateGraphView()
        {
            //Remove old objects
            if (_nodeObjects != null)
            {
                if (_nodeObjects.Count > 0)
                {
                    for (int i = _nodeObjects.Count; i < 0; i--)
                    {
                        DestroyImmediate(_nodeObjects[i]);
                    }
                }
            }
            
            if(_edgeObjects !=null)
            {
                if (_edgeObjects.Count > 0)
                {
                    for (int i = _edgeObjects.Count; i > 0; i--)
                    {
                        DestroyImmediate(_edgeObjects[i]);
                    }
                }
            }

            if (root == null)
            {
                root = GameObject.FindGameObjectWithTag("Graph");
            }
            
            for (int i = root.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(root.transform.GetChild(0).gameObject);
            }
            
            //Reset Dictionaries and lists
            _nodeObjects = new List<GameObject>();
            _edgeObjects = new List<GameObject>();
            _nodeDictionary = new Dictionary<NodeHook, GraphNode>();
            _edgeDictionary = new Dictionary<EdgeHook, GraphNode>();
            
            //Add new objects
            for (int i = 0; i < graph.GetNodes().Count; i++)
            {
                GameObject newNodeObject = Instantiate(nodePrefab, graph.GetNodes()[i].position, Quaternion.identity);
                newNodeObject.name = "Node - " + i;
                newNodeObject.transform.parent = root.transform;
                newNodeObject.GetComponent<NodeHook>().Init(this);
                
                _nodeDictionary.Add(newNodeObject.GetComponent<NodeHook>(), graph.GetNodes()[i]);
                
                //Add Edge Objects
                for (int j = 0; j < graph.GetEdgesFromNode(graph.GetNodes()[i]).Count; j++)
                {
                    GraphEdge _edge = graph.GetNodes()[i].edges[j];
                    GameObject newEdgeObject = Instantiate(edgePrefab, Vector3.zero, Quaternion.identity);
                    newEdgeObject.name = "Edge - " + j;
                    newEdgeObject.transform.parent = newNodeObject.transform;
                    newEdgeObject.GetComponent<EdgeHook>().Init(this, j, graph.GetNodes()[i].position, graph.GetNodes()[_edge.toNodeIndex].position, _edge.controlA, _edge.controlB);
                    
                    _edgeDictionary.Add(newEdgeObject.GetComponent<EdgeHook>(), graph.GetNodes()[i]);
                }
            }
        }
        
        
        public void SyncNode(NodeHook hook)
        {
            if (_nodeDictionary != null)
            {
                if (_nodeDictionary.ContainsKey(hook))
                {
                    graph.UpdateNode(_nodeDictionary[hook], hook.transform.position);
                }
            }
            
            UpdateGraphView();
        }
        public void DestroyNode(NodeHook nodeHook)
        {
            if(_nodeDictionary.ContainsKey(nodeHook))
            {
                this.RemoveNode(_nodeDictionary[nodeHook]);
            }
            
            UpdateGraphView();
        }
        public void AddEdge(NodeHook node)
        {
            if (_startNode == null)
            {
                _startNode = node;
            }
            else if(_startNode != node)
            {
                _endNode = node;
                
                //create new edge
                graph.AddEdge(_nodeDictionary[_startNode], _nodeDictionary[_endNode]);
                //reset object selection
                _startNode.addingEdge = false;
                _endNode.addingEdge = false;
                _startNode = null;
                _endNode = null;
                UpdateGraphView();

            }
        }
        
        public void SyncEdge(EdgeHook edge)
        {
            graph.UpdateEdge(_edgeDictionary[edge], edge.edgeIndex, edge.controlA, edge.controlB);
            
            UpdateGraphView();
        }

        public void DestroyEdge(EdgeHook edge)
        {
            graph.RemoveEdge(_edgeDictionary[edge], edge.edgeIndex);
            
            UpdateGraphView();
        }


        public void CopyEdge(EdgeHook edgeHook)
        {
            GraphNode startNode = _edgeDictionary[edgeHook];
            GraphEdge toNode = graph.GetEdgesFromNode(startNode)[edgeHook.edgeIndex];
            GraphNode endNode = graph.GetNodes()[toNode.toNodeIndex];

            for (int i = 0; i < endNode.edges.Count; i++)
            {
                if (endNode.edges[i].toNodeIndex == graph.GetNodes().IndexOf(startNode))
                {
                    graph.UpdateEdge(startNode, edgeHook.edgeIndex, endNode.edges[i].controlB, endNode.edges[i].controlA);
                }
            }
            
            UpdateGraphView();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(this.transform.position, 2);
        }

        private void OnEnable()
        {
            UpdateGraphView();
        }
    }
}
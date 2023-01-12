using UnityEditor;
using UnityEngine;
using Visuals;

namespace Editor_Tools
{
    [CustomEditor(typeof(GraphAPI))]
    public class GraphAPIEditorExtension : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GraphAPI myScript = (GraphAPI) target;
            if(GUILayout.Button("Add Node"))
            {
                myScript.AddNode();
            }

            if (GUILayout.Button("Synchronise"))
            {
                myScript.UpdateGraphView();
            }
        }
    }
    
    [CustomEditor(typeof(NodeHook))]
    public class NodeHookExtension : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NodeHook myScript = (NodeHook) target;
            if(GUILayout.Button("Sync Node"))
            {
                myScript.SyncNode();
            }

            if (GUILayout.Button("Add Edge"))
            {
                myScript.AddEdge();
            }

            if (GUILayout.Button("Remove Node"))
            {
                myScript.RemoveNode();
            }
        }
    }
    
    [CustomEditor(typeof(EdgeHook))]
    public class EdgeHookExtension : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EdgeHook myScript = (EdgeHook) target;
            if(GUILayout.Button("Sync Edge"))
            {
                myScript.SyncEdge();
            }

            if (GUILayout.Button("Copy Edge"))
            {
                myScript.CopyEdgeControlPoints();
            }
            
            if (GUILayout.Button("Remove Edge"))
            {
                myScript.RemoveEdge();
            }
        }
    }

}
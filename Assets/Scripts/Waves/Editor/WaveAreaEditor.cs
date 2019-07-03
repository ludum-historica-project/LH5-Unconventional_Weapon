using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveArea))]

public class WaveAreaEditor : Editor
{

    WaveArea tgt;

    private void OnEnable()
    {
        tgt = (WaveArea)target;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView view)
    {
        if (tgt.type == WaveArea.AreaType.Circle)
        {
            Handles.DrawWireDisc(tgt.center, Vector3.forward, tgt.radius);
        }
        else
        {
            Handles.DrawWireCube(tgt.center, tgt.size);
        }
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        tgt.center = EditorGUILayout.Vector2Field("Center", tgt.center);
        tgt.type = (WaveArea.AreaType)EditorGUILayout.EnumPopup(tgt.type);
        if (tgt.type == WaveArea.AreaType.Circle)
        {
            tgt.radius = EditorGUILayout.FloatField("Radius", tgt.radius);
        }
        else
        {
            tgt.size = EditorGUILayout.Vector2Field("Size", tgt.size);
            if (tgt.size.x <= 0) tgt.size.x = .01f;
            if (tgt.size.y <= 0) tgt.size.y = .01f;
        }
        if (GUI.changed)
        {
            SceneView.RepaintAll();
            EditorUtility.SetDirty(tgt);
        }

    }
}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SPlayerSave))]
public class ESPlayerSave : Editor
{
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        //GUILayout.BeginHorizontal();
        base.OnInspectorGUI();
        //GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        SPlayerSave save = target as SPlayerSave;
        if (GUILayout.Button("Apply", GUILayout.MinWidth(80)))
        {
            Debug.Log("押した!");
            save.WriteData();
        }
        
        if (GUILayout.Button("Load", GUILayout.MinWidth(80)))
        {
            Debug.Log("押した!");
            save.LoadData();
        }
        GUILayout.EndHorizontal();//DrawDefaultInspector();
    }
}

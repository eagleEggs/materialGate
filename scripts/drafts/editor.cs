using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(matGateController))]
public class matGateEditor : Editor
{
    private int selected = 0;
    private int ePSelected = 0;
    private string saveName = "Save Name";
    private bool showCurves = false;
    public float x;
    private int prefCount;
    //matGateController matGate;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        matGateController matGate = (matGateController)target;

        string[] edPrefs = new string[]
        
        {
                "1: " + EditorPrefs.GetString(" "), "2: " + EditorPrefs.GetString(" 2"), "3: " + EditorPrefs.GetString(" 3")
          //"xF:"+ EditorPrefs.GetFloat("xF") + " yF: " + EditorPrefs.GetFloat("yF") + " wXF: " + EditorPrefs.GetFloat("wXF") + " wYF: " + EditorPrefs.GetFloat("wYF")

        };


            ePSelected = EditorGUILayout.Popup("Store:", ePSelected, edPrefs, EditorStyles.popup);

        
        string[] options = new string[]
        {
         "Default", "Relative", "Crazy",
        };
        selected = EditorGUILayout.Popup("Algoritm:", selected, options, EditorStyles.popup);
        if (selected == 0) {  }
        if (selected == 1) { Debug.Log("1"); }
        if (selected == 2) { Debug.Log("2"); }

        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Update", GUILayout.Width(100), GUILayout.Height(20)))
        {
            matGate.pushGate();
        }
        if (GUILayout.Button("Rand. Small", GUILayout.Width(100), GUILayout.Height(20)))
        {
            matGate.pushGate_random();
        }
        if (GUILayout.Button("Rand. Large", GUILayout.Width(100), GUILayout.Height(20)))
        {
            matGate.pushGate_random();
        }
        if (GUILayout.Button("Rand. Pixel", GUILayout.Width(100), GUILayout.Height(20)))
        {
            matGate.pushGate_randomPixel();
        }
        if (GUILayout.Button("Reset Prefs", GUILayout.Width(100), GUILayout.Height(20)))
        {
            prefCount = 0;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginVertical("box");
        if (GUILayout.Button("Save", GUILayout.Width(400), GUILayout.Height(20)))
        {
            matGate.pushGate_save(saveName);
        }

        saveName = GUILayout.TextField(saveName, 25, GUILayout.Width(400), GUILayout.Height(20));
        if (GUILayout.Button("Save Prefs", GUILayout.Width(400), GUILayout.Height(20)))
        {
            
                prefCount++;
                EditorPrefs.SetString("" + prefCount, "xF:" + EditorPrefs.GetFloat("xF") + " yF: " + EditorPrefs.GetFloat("yF") + " wXF: " + EditorPrefs.GetFloat("wXF") + " wYF: " + EditorPrefs.GetFloat("wYF"));
                //EditorPrefs.SetString(" prefCount, "xF:" + EditorPrefs.GetFloat("xF") + " yF: " + EditorPrefs.GetFloat("yF") + " wXF: " + EditorPrefs.GetFloat("wXF") + " wYF: " + EditorPrefs.GetFloat("wYF"));
                //EditorPrefs.SetString("3", "xF:" + EditorPrefs.GetFloat("xF") + " yF: " + EditorPrefs.GetFloat("yF") + " wXF: " + EditorPrefs.GetFloat("wXF") + " wYF: " + EditorPrefs.GetFloat("wYF"));
                EditorPrefs.SetFloat("xF" + prefCount, matGate.xFrac);
                EditorPrefs.SetFloat("yF" + prefCount, matGate.yFrac);
                EditorPrefs.SetFloat("wXF" + prefCount, matGate.warpXFrac);
                EditorPrefs.SetFloat("wYF" + prefCount, matGate.warpYFrac);
                Debug.Log("Saving...");
                Debug.Log(matGate.xFrac);
                Debug.Log("PrefCount:" + prefCount);

            


        }
        if (GUILayout.Button("Load Prefs", GUILayout.Width(400), GUILayout.Height(20)))
        {
            float x = EditorPrefs.GetFloat("xF", matGate.xFrac);
            EditorPrefs.GetFloat("yF");
            EditorPrefs.GetFloat("wXF");
            EditorPrefs.GetFloat("wYF");
            Debug.Log("Loading...");
            matGate.xFrac = x;
            Debug.Log(x);
            
        }

        GUILayout.EndVertical();

    }

    void AddCurveToSelectedGameObject()
    {


    }

    void OnValidate()
    {

       


    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(matGateController))]
public class matGateEditor : Editor {

	private string saveName = "Save Name";
	private bool showCurves = false;


	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        

        matGateController matGate = (matGateController)target;


        	if(GUILayout.Button("Update"))
        {
            	matGate.pushGate();
        }
        	if(GUILayout.Button("Randomize Small"))
        {
            	matGate.pushGate_random();
        }
        	if(GUILayout.Button("Randomize Large"))
        {
            	matGate.pushGate_random();
        }
        	if(GUILayout.Button("Randomize Pixel"))
        {
            	matGate.pushGate_randomPixel();
        }
             if(GUILayout.Button("Save"))
        {
            	matGate.pushGate_save(saveName);
        }
        
        saveName = GUILayout.TextField(saveName, 25);


    }

    void  AddCurveToSelectedGameObject(){


    }







}
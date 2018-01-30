using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(matGateController))]
public class matGateEditor : Editor {

	private string saveName = "Enter Save Name"; // save name string

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // initialize Inspector
        

        matGateController matGate = (matGateController)target; // attach matGateController

        // Various Button:
        	if(GUILayout.Button("Update")) // Updates the texture with the changed values in edit mode
        {
            	matGate.pushGate();
        }
        	if(GUILayout.Button("Randomize Small")) // randomizes to a small degree
        {
            	matGate.pushGate_randomSmall();
        }
        	if(GUILayout.Button("Randomize Large")) // randomizes to a larger degree
        {
            	matGate.pushGate_random();
        }
        	if(GUILayout.Button("Randomize Pixel")) // randomly pixellates the entire texture
        {
            	matGate.pushGate_randomPixel();
        }
             if(GUILayout.Button("Save")) // save the texture with the save named entered as the string
        {
            	matGate.pushGate_save(saveName);
        }
        
        saveName = GUILayout.TextField(saveName, 25); // max filename length is 25 characters


    }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(matGateController))]
public class matGateEditorWindow : EditorWindow {


	AnimationCurve curveX = AnimationCurve.Linear(0, 0, 10, 10);
    AnimationCurve curveY = AnimationCurve.Linear(0, 0, 10, 10);
    AnimationCurve curveZ = AnimationCurve.Linear(0, 0, 10, 10);

    [MenuItem("materialGate/curveEditor")]
	static void Init()
    {
    	//matGateController matGate = (matGateController)target;
        matGateEditorWindow window = (matGateEditorWindow)EditorWindow.GetWindow(typeof(matGateEditorWindow));
        window.Show();
    }

	void OnGUI()
    {

        
        curveX = EditorGUILayout.CurveField("Animation on X", curveX);
        curveY = EditorGUILayout.CurveField("Animation on Y", curveY);
        curveZ = EditorGUILayout.CurveField("Animation on Z", curveZ);

        if (GUILayout.Button("Generate Curve")){
        	//matGate.warpFactor = curveX;
        	//matGate.pushGate();
            AddCurveToSelectedGameObject();
        }
    	
        


    }

    void  AddCurveToSelectedGameObject(){

    	if (Selection.activeGameObject)
        {
            matGateController comp = Selection.activeGameObject.AddComponent<matGateController>();

            comp.SetCurves(curveX, curveY, curveZ);
            Debug.Log("Setting Curves");
        }
        else
        {
            Debug.LogError("No Game Object selected for adding an animation curve");
        }
    }



    }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(matGateController))]
public class matGateEditor : Editor {

	private string saveName = "Enter Save Name"; // save name string

    //mode7:
    private bool isAnimating = false;
    private Mode7Config startConfig;
    private Mode7Config targetConfig;

    private Color col1 = new Color(0.5f, 0.5f, 0.5f);
    private Color col2 = new Color(0.7f, 0.7f, 0.7f);
    private Color colSelected = new Color(0.3f, 0.3f, 0.3f);

    private int selectedIndex = 0;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // initialize Inspector


        matGateController matGate = (matGateController)target; // attach matGateController

        // Various Button:
        if (GUILayout.Button("Update")) // Updates the texture with the changed values in edit mode
        {
            matGate.pushGate();
        }
        if (GUILayout.Button("Randomize Small")) // randomizes to a small degree
        {
            matGate.pushGate_randomSmall();
        }
        if (GUILayout.Button("Randomize Large")) // randomizes to a larger degree
        {
            matGate.pushGate_random();
        }
        if (GUILayout.Button("Randomize Pixel")) // randomly pixellates the entire texture
        {
            matGate.pushGate_randomPixel();
        }
        if (GUILayout.Button("Save")) // save the texture with the save named entered as the string
        {
            matGate.pushGate_save(saveName);
        }

        saveName = GUILayout.TextField(saveName, 25); // max filename length is 25 characters


        //mode7:

        col1 = EditorGUILayout.ColorField("Odd row colour", col1);
        col2 = EditorGUILayout.ColorField("Even row colour", col2);
        colSelected = EditorGUILayout.ColorField("Selected row colour", colSelected);

        //Mode7Controller controller = (Mode7Controller)target;

        matGate.animationCurve = EditorGUILayout.CurveField("Animation Curve", matGate.animationCurve, Color.green, new Rect(0, 0, 1, 1), GUILayout.Height(100));

        matGate.animationTime = Mathf.Max(EditorGUILayout.DelayedFloatField("Animation time", matGate.animationTime), 0);

        float buttonWidth = 25;
        float fieldWidth = 40;
        float configHeight = 10;
        float spacing = 5;

        GUIStyle numberFieldStyle = new GUIStyle(EditorStyles.numberField)
        {
            alignment = TextAnchor.MiddleCenter
        };

        GUIStyle richTextButtonStyle = new GUIStyle(GUI.skin.button)
        {
            richText = true
        };

        GUIStyle middleAlignedText = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            richText = true
        };

        Rect r = GUILayoutUtility.GetRect(buttonWidth, configHeight + spacing);
        string[] labels = new string[] { "H", "V", "X<size=7>0</size>", "Y<size=7>0</size>", "A", "B", "C", "D" };

        for (int i = 0; i < labels.Length; i++)
        {
            Rect labelRect = new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * i, r.y, fieldWidth, r.height);
            EditorGUI.DrawRect(labelRect, Color.grey);
            EditorGUI.LabelField(labelRect, labels[i], middleAlignedText);
        }

        if (GUI.Button(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 8, r.y, 30, r.height), "+", richTextButtonStyle))
        {
            matGate.configs.Insert(0, new Mode7Config());
            selectedIndex++;
        }

        for (int i = 0; i < matGate.configs.Count; i++)
        {
            r = GUILayoutUtility.GetRect(buttonWidth, configHeight + spacing);

            EditorGUI.DrawRect(new Rect(r.x, r.y, r.width, r.height), i == selectedIndex ? colSelected : (((i & 1) > 0) ? col1 : col2));

            string buttonText;

            if (i != selectedIndex)
            {
                buttonText = i.ToString();
            }
            else
            {
                buttonText = "<color=" + (isAnimating ? "red" : "black") + "><b>" + i.ToString() + "</b></color>";
            }

            if (GUI.Button(new Rect(r.x, r.y, buttonWidth, r.height), buttonText, richTextButtonStyle))
            {
                selectedIndex = i;
                //Debug.LogFormat("Animation start at {0}", Time.time);
                isAnimating = true;
                matGate.animationStartTime = Time.time;
                startConfig = matGate.GetConfig();
                targetConfig = matGate.configs[i];
            }

            #region Drawing Mode7Config fields and updating the config
            Mode7Config config = matGate.configs[i];
            config.h = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 0, r.y, fieldWidth, r.height), config.h, numberFieldStyle);
            config.v = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 1, r.y, fieldWidth, r.height), config.v, numberFieldStyle);
            config.x0 = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 2, r.y, fieldWidth, r.height), config.x0, numberFieldStyle);
            config.y0 = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 3, r.y, fieldWidth, r.height), config.y0, numberFieldStyle);
            config.a = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 4, r.y, fieldWidth, r.height), config.a, numberFieldStyle);
            config.b = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 5, r.y, fieldWidth, r.height), config.b, numberFieldStyle);
            config.c = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 6, r.y, fieldWidth, r.height), config.c, numberFieldStyle);
            config.d = EditorGUI.FloatField(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 7, r.y, fieldWidth, r.height), config.d, numberFieldStyle);
            if (config != matGate.configs[i])
            {
                //Debug.Log("Value updated");
                matGate.configs[i] = config;
                if (i == selectedIndex)
                {
                    UpdateConfig(matGate);
                }
            }
            #endregion

            if (GUI.Button(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 8, r.y, 30, r.height), "+", richTextButtonStyle))
            {
                matGate.configs.Insert(i + 1, new Mode7Config());
                if (selectedIndex > i)
                {
                    selectedIndex++;
                }
            }

            if (GUI.Button(new Rect(buttonWidth + 20 + spacing + (spacing + fieldWidth) * 8 + 30 + spacing, r.y, 30, r.height), "-", richTextButtonStyle))
            {
                matGate.configs.RemoveAt(i);
                selectedIndex = Mathf.Max(0, selectedIndex - 1);
                UpdateConfig(matGate);
            }
        }

        if (isAnimating)
        {
            if (matGate.animationStartTime + matGate.animationTime < Time.time)
            {
                //Debug.Log("Animation end");
                isAnimating = false;
            }
            else
            {
                Mode7Config config = matGate.InterpolateFromTo(startConfig, targetConfig, (Time.time - matGate.animationStartTime) / matGate.animationTime);
                matGate.SetConfig(config);
            }
            Repaint();
        }
    }
    public void UpdateConfig(matGateController matGate)
        {
        if (selectedIndex >= 0 && selectedIndex < matGate.configs.Count - 1)
            {
            matGate.SetConfig(matGate.configs[selectedIndex]);
            }
        }






}
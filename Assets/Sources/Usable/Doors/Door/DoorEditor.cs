using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

/*[CustomEditor(typeof(DoorAudioController))]
public class DoorAudioControllerEditor : Editor
{
    string[] Sounds = new string[] { "Open", "Close", "Locked"};
    bool ElementsExpand = true;
    GUIContent[] Var = new GUIContent[3];

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
       
        DoorAudioController myTarget = (DoorAudioController)target;
 
        SerializedObject AudioClipObject = new SerializedObject(myTarget);
        SerializedProperty AudioClipArray = AudioClipObject.FindProperty("AudioClips");

        AudioClipArray.arraySize = Sounds.Length;

        ElementsExpand = EditorGUILayout.Foldout(ElementsExpand, "Door Sounds");

        if(ElementsExpand)
        {
            if (Selection.activeTransform)
            {
                AudioClipArray.NextVisible(true);
                EditorGUI.indentLevel += 1;

                for (int i = 0; i < Sounds.Length; i++)
                {
                    AudioClipArray.NextVisible(true);

                    Var[i] = new GUIContent(Sounds[i]);

                    EditorGUILayout.PropertyField(AudioClipArray, Var[i], true);
                }
            }
        }
        else
        {
            if (!Selection.activeTransform)
            {
                AudioClipArray.NextVisible(false);
            }
        }
 
    }
}*/

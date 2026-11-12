using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;
using JsonUtility = UnityEditor.EditorUtility;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue Asset", order = 1)]
public class MasterDialogue : ScriptableObject
{
    public string dialogueAssetName = "new Dialogue";
    public List<SceneData> sceneData = new List<SceneData>();
    public List<LanguageData>  languageData = new List<LanguageData>();
#if UNITY_EDITOR
    private void Reset()
    {
        Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/DialogueTool/Icons/DialogueAssetIcon.png");
        if (icon != null)
        {
            EditorGUIUtility.SetIconForObject(this, icon);
        }
    }
#endif
}


[Serializable]
public class SceneData
{
    public string SceneName;
    public List<CharacterData> CharacterList = new List<CharacterData>();

    public List<DialogueData> DialogueList = new List<DialogueData>();
}

[Serializable]
public class CharacterData
{
    public string Name;
    public Color Colour;
}

[Serializable]
public class DialogueData : Node
{
    public string Name;
    public string dialogueText;
    public Color Colour;
    public Vector3 nodePosition;

    public List<string> childNodeIds;
}

[Serializable]
public class LanguageData
{
    public string LanguageName;
}

[CustomEditor(typeof(MasterDialogue))]
public class CustomDialogue : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MasterDialogue masterDialogue = (MasterDialogue)target;

        GUILayout.Space(10);
        if (GUILayout.Button("Edit Asset window", GUILayout.Height(30)))
        {
        }
    }
}
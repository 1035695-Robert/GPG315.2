using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DialogueSystem.Enumerators;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue Asset", order = 1)]
public class MasterDialogueData : ScriptableObject
{
    [field: SerializeField] public string FileName { get; set; }
  [field: SerializeField]  public List<SceneData> sceneData { get; set; } = new();
   [field:SerializeField] public List<LanguageData> languageData { get; set; } = new();

    public void Initialize(string fileName)
    {
        FileName = fileName;
        sceneData = new List<SceneData>();
        languageData = new List<LanguageData>();
    }
}

public class SceneData : ScriptableObject
{
    [field: SerializeField] public string ID { get; set; }
    [field: SerializeField] public string SceneName { get; set; }

    [field: SerializeField] public List<CharacterData> CharacterList { get; set; } = new();
    [field: SerializeField] public List<DialogueData> DialogueList { get; set; } = new();

    [field: SerializeField] public List<string> OldNodeNames { get; set; }


    public void Initialize(string sceneName)
    {
        SceneName = sceneName;

        CharacterList = new List<CharacterData>();
        DialogueList = new List<DialogueData>();
    }
}
[Serializable]
public class CharacterData
{
    public string Name { get; set; }
    public Color Colour { get; set; }
    public bool IsAllowedMultiChoice { get; set; }
}
[Serializable]
public class DialogueData
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string DialogueText { get; set; }
    public List<ChoiceSaveData> Choices { get; set; }
    public Color Colour { get; set; }
    public DialogueType Type { get; set; }
    public Vector3 Position { get; set; }
}

public class ChoiceSaveData
{
    public string Text { get; set; }
    public string NodeId { get; set; }
}


public class LanguageData
{
    public string LanguageName;
}


[CustomEditor(typeof(MasterDialogueData))]
public class CustomDialogue : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MasterDialogueData masterDialogueData = (MasterDialogueData)target;

        GUILayout.Space(10);
        if (GUILayout.Button("Edit Asset window", GUILayout.Height(30)))
        {
        }
    }
}
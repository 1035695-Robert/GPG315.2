using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using DialogueSystem.Enumerators;

public class DialogueScriptableObject : ScriptableObject
{
    [field: SerializeField] public string DialogueName { get; set; }
    [field: SerializeField] public string DialogueText { get; set; }
    [field: SerializeField] public List<DialogueChoiceData> Choices { get; set; }
    [field: SerializeField] public DialogueType Type { get; set; }
    [field: SerializeField] public bool IsStartingDialogue { get; set; }

    public void Initialize(string dialogueName, string text, List<DialogueChoiceData> choices, DialogueType type,
        bool isStartingDialogue)
    {
        DialogueName = dialogueName;
        DialogueText = text;
        Choices = choices;
        Type = type;
        IsStartingDialogue = isStartingDialogue;
    }
}

[Serializable]
public class DialogueChoiceData
{
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public DialogueScriptableObject nextDialogue { get; set; }
}
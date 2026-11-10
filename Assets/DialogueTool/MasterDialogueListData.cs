using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[Serializable]
public class MasterDialogueListData : ScriptableObject
{
        public string SceneName;
        public List<CharacterData>  CharacterList = new List<CharacterData>();
       
        [SerializeReference]public List<DialogueData>  DialogueList = new List<DialogueData>();
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
public class LanguageData: ScriptableObject
{
        public string LanguageName;
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDialogueListData : ScriptableObject
{
        public string SceneName;
        public List<CharacterData>  CharacterList = new List<CharacterData>();
        public List<DialogueData>  DialogueList = new List<DialogueData>();
}
[Serializable]
public class CharacterData
{
        public string Name;
        public Color Colour;
}

[Serializable]
public class DialogueData
{
        public string dialogueText;
}


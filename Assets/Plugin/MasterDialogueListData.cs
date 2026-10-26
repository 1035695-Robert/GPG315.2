using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDialogueListData
{
        public string DialogueName;
        public List<CharacterData>  DialogueList = new List<CharacterData>();
}
[Serializable]
public class CharacterData
{
        public string Name;
        public Color Colour;
}


using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDialogueListData : ScriptableObject
{
        public string SceneName;
        public List<CharacterData>  CharacterList = new List<CharacterData>();
}
[Serializable]
public class CharacterData
{
        public string Name;
        public Color Colour;
}


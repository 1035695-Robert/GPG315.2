using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDialogueListData : ScriptableObject
{
        public List<SceneData> sceneList = new List<SceneData>();
        public List<LanguageData>  LanguagesList  = new List<LanguageData>();
}

[Serializable]
public class SceneData
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
[Serializable]
public class LanguageData
{
        public string LanguageName;
        public List<DialogueData>  DialogueList = new List<DialogueData>();
}
[Serializable]
public class DialogueData
{
        public string dialogueText;
}


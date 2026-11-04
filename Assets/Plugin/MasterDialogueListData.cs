using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDialogueListData 
{  
        public List<LanguageData> LanguagesFiles = new List<LanguageData>();
        public List<SceneData> sceneList = new List<SceneData>(); 
}

[Serializable]
public class SceneData : ScriptableObject
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
public class LanguageData
{
        public string LanguageName;
}
[Serializable]
public class DialogueData
{
        public string speakerName;
        public string dialogueText;
}


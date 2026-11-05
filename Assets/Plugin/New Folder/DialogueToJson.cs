using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogueToJson
{
    public void SaveToJson(MasterDialogueListData data)
    {
        if (!AssetDatabase.IsValidFolder("Assets/Dialogue"))
        {
            AssetDatabase.CreateFolder("Assets", "Dialogue");
        }

        foreach (string language in data.LanguagesFiles.Select(lang => lang.LanguageName))
        {
            Debug.Log("Saving language: " + language);
            if (!AssetDatabase.IsValidFolder("Assets/Dialogue/" + language))
                AssetDatabase.CreateFolder("Assets/Dialogue", language);
        }


        
        foreach (string scene in data.sceneList.Select(lang => lang.SceneName))
        {
            string dialogueData = JsonUtility.ToJson(data);
            string filePath = Path.Combine(Application.dataPath + "Dialogue/" +
                                           scene);
            Debug.Log("file path: " + filePath);
            File.WriteAllText(filePath, dialogueData);
        }
    }
} 
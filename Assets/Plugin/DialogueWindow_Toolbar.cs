using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

public partial class DialogueWindow
{
    private void CreateToolBar()
    {
        popupOverlay = new VisualElement();
        popupOverlay.AddToClassList("popup-overlay");
        popupOverlay.style.display = DisplayStyle.None;

        VisualElement popupwindow = new VisualElement();
        popupwindow.AddToClassList("popup-window");


        Label title = new Label { text = "title", };
        newLangaugeName = new TextField { label = "name", style = { flexDirection = FlexDirection.Column } };
        Button createButton = new Button(() => CreateNewLanguage()) { text = "create" };

        popupwindow.Add(title);
        popupwindow.Add(newLangaugeName);
        popupwindow.Add(createButton);

        popupOverlay.Add(popupwindow);


        LanguageDropDown();
        SaveButton();
    }

    private void SaveButton()
    {
        var SaveButton = new ToolbarButton
        {
            text = "Save Dialogue",
            style =
            {
                marginLeft = StyleKeyword.Auto,
                width = 100,
            }
        };
        SaveButton.clicked += SaveDialogue;
        tools.Add(SaveButton);
    }

    private void SaveDialogue()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Dialogue"))
        {
            AssetDatabase.CreateFolder("Assets", "Dialogue");
        }

        foreach (string language in masterDialogueListData.LanguagesFiles.Select(lang => lang.LanguageName))
        {
            Debug.Log("Saving language: " + language);
            if (!AssetDatabase.IsValidFolder("Assets/Dialogue/" + language))
                AssetDatabase.CreateFolder("Assets/Dialogue", language);
        }


        string dialogueData = JsonUtility.ToJson(masterDialogueListData,true);
        string filePath = Path.Combine(Application.streamingAssetsPath,  "scene.json");
        Debug.Log("file path: " + filePath);
        File.WriteAllText(filePath, dialogueData);
    }
}
using System;
using System.Collections.Generic;
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
        Debug.Log("Save Dialogue");
    }
}
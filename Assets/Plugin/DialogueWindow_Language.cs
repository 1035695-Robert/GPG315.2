using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    private void LanguageDropDown()
    {
        var languageButton = new ToolbarButton
        {
            text = "Language",
            iconImage = EditorGUIUtility.IconContent("d_icon dropdown").image as Texture2D,
            style = { flexDirection = FlexDirection.RowReverse }
        };
        
      languageButton.clicked += OpenDropdown(languageButton);
        tools.Add(languageButton);
        rootVisualElement.Add(tools);
    }

    private void OpenDropdown(ToolbarButton languageButton)
    {
        
    }


    private void AddLanguage(DropdownMenuAction action)
    {
        Debug.Log("Add language");
    }
    private void OnFocusIn(FocusInEvent evt)
    {
        var button = evt.currentTarget as ToolbarButton;
        button.style.backgroundColor = Color.white;
    }
}
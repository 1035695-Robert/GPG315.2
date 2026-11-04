using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

public partial class DialogueWindow
{
    private void LanguageDropDown()
    {
       var languageMenu = new ToolbarMenu()
        {
            text = "Language",
            //iconImage = EditorGUIUtility.IconContent("d_icon dropdown").image as Texture2D,
            style =
            {
                width = 100
            }
        };
        
        languageMenu.menu.AppendAction("Add Language", menuEvent =>
        {
            SetPopupVisibility(true);
        });
        tools.Add(languageMenu);
        rootVisualElement.Add(tools);
    }

    private void CreateNewLanguage()
    { 
        LanguageData newLanguage = new LanguageData();
        newLanguage.LanguageName = newLangaugeName.value;
        masterDialogueListData.LanguagesFiles.Add(newLanguage);
        Debug.Log("new language: " + newLanguage.LanguageName);
        SetPopupVisibility(false);
    }
    
    private void SetPopupVisibility(bool visibility)
    {
        popupOverlay.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
    }
    
    
    private void RefreshLanguages()
    {
        
    }

}
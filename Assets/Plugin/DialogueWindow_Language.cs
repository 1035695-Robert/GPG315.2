using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
private ToolbarMenu languageMenu;
private VisualElement popupOverlay;

private void CreateNewLanguageMenuPopup()
{
    popupOverlay = new VisualElement();
    popupOverlay.AddToClassList("popup-overlay");
    popupOverlay.style.display = DisplayStyle.None;

    VisualElement popupwindow = new VisualElement();
    popupwindow.AddToClassList("popup-window");


    Label title = new Label { text = "title", };
    var newLangaugeName = new TextField { label = "name", style = { flexDirection = FlexDirection.Column } };
    Button createButton = new Button(() => CreateNewLanguage()) { text = "create" };

    popupwindow.Add(title);
    popupwindow.Add(newLangaugeName);
    popupwindow.Add(createButton);

    popupOverlay.Add(popupwindow);


    LanguageDropDown();
    SaveButton();
}
    private void LanguageDropDown()
    {
        languageMenu = new ToolbarMenu()
        {
            text = "Language",
            style =
            {
                width = 100
            }
        };
        
        languageMenu.menu.AppendAction("Add Language", menuEvent =>
        {
            //SetPopupVisibility(true);
        });
        tools.Add(languageMenu);
        rootVisualElement.Add(tools);
    }

    private void CreateNewLanguage()
    { 
        // LanguageData newLanguage = new LanguageData();
        // newLanguage.LanguageName = newLangaugeName.value;
        // masterDialogueListData.LanguagesFiles.Add(newLanguage);
        // Debug.Log("new language: " + newLanguage.LanguageName);
        // Debug.Log(string.Join(",", masterDialogueListData.LanguagesFiles.Select(lang => lang.LanguageName)));
        //
        // DisplayLanguages();
        //
        // SetPopupVisibility(false);
    }

    private void DisplayLanguages()
    {//creates a 
    //     foreach (string language in masterDialogueListData.LanguagesFiles.Select(lang => lang.LanguageName))
    //     {
    //         languageMenu.menu.AppendAction(language, menuEvent =>
    //         {
    //             SelectLanguage(language);
    //         });
    //     }
    }

    // private void SelectLanguage(string language)
    // {
    //     Debug.Log("selected language: " + language);
    // }
    //
    // private void SetPopupVisibility(bool visibility)
    // {
    //     popupOverlay.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
    // }
}

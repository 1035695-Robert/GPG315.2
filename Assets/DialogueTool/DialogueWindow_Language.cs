using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    private ToolbarMenu _languageMenu;
    private VisualElement _popupOverlay;

    private void CreateNewLanguageMenuPopup()
    {
        _popupOverlay = new VisualElement(); 
        _popupOverlay.AddToClassList("popup-overlay");
        _popupOverlay.style.display = DisplayStyle.None;

        VisualElement popupwindow = new VisualElement();
        popupwindow.AddToClassList("popup-window");


        Label title = new Label { text = "title", };
        var newLangaugeName = new TextField { label = "name", style = { flexDirection = FlexDirection.Column } };
        Button createButton = new Button(() => CreateNewLanguage(newLangaugeName.value)) { text = "create" };

        popupwindow.Add(title);
        popupwindow.Add(newLangaugeName);
        popupwindow.Add(createButton);

        _popupOverlay.Add(popupwindow);
    }

    private void LanguageDropDown()
    {
        var toolbar = new Toolbar();
        _languageMenu = new ToolbarMenu()
        {
            text = "Language",
            style =
            {
                width = 100
            }
        };

        _languageMenu.menu.AppendAction("Add Language", menuEvent => { SetPopupVisibility(true); });
        tools.Add(_languageMenu);
        rootVisualElement.Add(tools);
    }

    private void CreateNewLanguage(string  newLangaugeName)
    {
        var newLanguage = CreateInstance<LanguageData>();
        newLanguage.LanguageName = newLangaugeName;
        
        languageList.Add(newLanguage);
        Debug.Log("new language: " + newLanguage.LanguageName);
        //Debug.Log(string.Join(",", masterDialogueListData.LanguagesFiles.Select(lang => lang.LanguageName)));

        DisplayLanguages();
        SetPopupVisibility(false);
    }

    private void DisplayLanguages()
    {
        //creates a 
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
    private void SetPopupVisibility(bool visibility)
    {
        _popupOverlay.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
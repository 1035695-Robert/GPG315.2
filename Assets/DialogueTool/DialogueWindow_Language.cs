using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    private ToolbarMenu _languageMenu;
    private VisualElement _popupOverlay;
    private string _currentSelectedLanguage;

    private void CreateNewLanguageMenuPopup()
    {
        _popupOverlay = new VisualElement();
        _popupOverlay.AddToClassList("popup-overlay");
        _popupOverlay.style.display = DisplayStyle.None;

        VisualElement popupwindow = new VisualElement();
        popupwindow.AddToClassList("popup-window");


        Label title = new Label { text = "title", };
        var newLangaugeName = new TextField { label = "name", style = { flexDirection = FlexDirection.Column } };
        var buttons = new VisualElement();
        buttons.AddToClassList("horizontal-button-container");

        Button cancel = new Button(CancelNewLanguage) { text = "Cancel" };
        cancel.AddToClassList("button-menu");

        Button createButton = new Button(() => CreateNewLanguage(newLangaugeName.value)) { text = "create" };
        createButton.AddToClassList("button-menu");


        buttons.Add(cancel);
        buttons.Add(createButton);

        popupwindow.Add(title);
        popupwindow.Add(newLangaugeName);
        popupwindow.Add(buttons);

        _popupOverlay.Add(popupwindow);
    }

    private void CancelNewLanguage()
    {
        SetPopupVisibility(false);
    }

    private void LanguageDropDown()
    {
        _languageMenu = new ToolbarMenu()
        {
            text = "Language",
            style =
            {
                justifyContent = Justify.Center,
                alignItems = Align.Center,
                width = 100
            }
        };
        _languageMenu.Q<VisualElement>(className: "unity-toolbar-menu__text").style.unityTextAlign =
            TextAnchor.MiddleCenter;

        _tools.Add(_languageMenu);

        rootVisualElement.Add(_tools);

        DisplayLanguages();
    }

    private void CreateNewLanguage(string newLangaugeName)
    {
        if (string.IsNullOrWhiteSpace(newLangaugeName)) return;
        var newLanguage = new LanguageData();

        newLanguage.LanguageName = newLangaugeName.Trim();

        masterDialogueObject.languageData.Add(newLanguage);
        Debug.Log("new language: " + newLanguage.LanguageName);
        Debug.Log(string.Join(",", masterDialogueObject.languageData.Select(lang => lang.LanguageName)));

        DisplayLanguages();
        SetPopupVisibility(false);
    }

    private void DisplayLanguages()
    {
        if(masterDialogueObject == null ||masterDialogueObject.languageData == null || _languageMenu ==  null ) return;
       
        _languageMenu.menu.MenuItems().Clear();
        foreach (string language in masterDialogueObject.languageData.Select(lang => lang.LanguageName))
        {
            _languageMenu.menu.AppendAction(language,
                action =>
                {
                    _currentSelectedLanguage = language;
                    SelectLanguage();
                },
                action => _currentSelectedLanguage == language
                    ? DropdownMenuAction.Status.Checked
                    : DropdownMenuAction.Status.Normal
            );
        }

        _languageMenu.menu.allowDuplicateNames = false;
        _languageMenu.menu.AppendSeparator();
        _languageMenu.menu.AppendAction("Add Language", menuEvent => { SetPopupVisibility(true); });
        _languageMenu.menu.AppendAction("Delete", action => { DeleteLanguage(); },
            action => _currentSelectedLanguage == null
                ? DropdownMenuAction.Status.Disabled
                : DropdownMenuAction.Status.Normal);
    }

    private void SelectLanguage()
    {
        _languageMenu.text = string.IsNullOrEmpty(_currentSelectedLanguage) ? "Language" : _currentSelectedLanguage;
        Debug.Log("selected language: " + _currentSelectedLanguage);
    }

    private void DeleteLanguage()
    {
        var selected = masterDialogueObject.languageData.FirstOrDefault(lang => lang.LanguageName == _currentSelectedLanguage);

        if (selected != null)
        {
            Debug.Log("Delete: " + selected.LanguageName);
            masterDialogueObject.languageData.Remove(selected);
        }

        _currentSelectedLanguage = null;
        DisplayLanguages();
    }

    private void SetPopupVisibility(bool visibility)
    {
        _popupOverlay.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
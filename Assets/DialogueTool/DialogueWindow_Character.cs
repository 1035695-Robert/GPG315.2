using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;


public partial class DialogueWindow
{
    EditorToolbarButton addCharacterButton;


    private TextField textName;

    // private List<CharacterData> charRow = scenelistData.d
    private VisualElement CharacterHeader()
    {
        var charRow = new VisualElement();
        charRow.AddToClassList("column-header");
        charRow.Add(new Label
        {
            text = "Add Character", style =
            {
                flexGrow = 1,
                marginLeft = 4,
                unityTextAlign = TextAnchor.MiddleLeft
            }
        });

        addCharacterButton = new EditorToolbarButton(() => AddCharacter())
        {
            iconImage = EditorGUIUtility.IconContent("d_Toolbar Plus").image as Texture2D,
        };


        addCharacterButton.AddToClassList("header-buttons");
        charRow.Add(addCharacterButton);
        return charRow;
    }

    private ColorField colourField;

    private ListView CreateCharacterList()
    {
        _charListView = new ListView
        {
            reorderable = true,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,

            makeItem = () =>
            {
                var charDropdown = new Foldout()
                { 
                    name = "char-dropdown",
                    text =  "New Character",
                    value = false
                };
                
                var nameContainer = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };
                nameContainer.Add(new Label{text  = "Name", style = { unityTextAlign = TextAnchor.MiddleLeft}});
                var headerName = new TextField
                {
                    name = "character-name-field",
                    
                    style =
                    {
                        flexDirection = FlexDirection.RowReverse,
                        flexGrow = 1,
                        marginRight = 4,
                        alignContent = Align.FlexStart
                    },
                };
                
                nameContainer.Add(headerName);
                charDropdown.Add(nameContainer);
                
                colourField = new ColorField
                {
                    name = "character-colour-field",
                    value =  Color.white,
                };

                charDropdown.Add(colourField);

                var addNode = (new Button()
                {
                    text = "Add Character",
                    style =
                    {
                        height = 25,
                        alignContent = Align.Center
                    }
                });
                charDropdown.Add(addNode);
                return charDropdown;
            },
            
            bindItem = (element, index) =>
            {
                var foldout = element.Q<Foldout>("char-dropdown");
                var textField = element.Q<TextField>("character-name-field");
                var colour = element.Q<ColorField>("character-colour-field");
                var button = element.Q<Button>();
                
                if (index < _selectedSceneData.CharacterList.Count && _selectedSceneData.CharacterList[index] != null)
                {
                    
                    var character = _selectedSceneData.CharacterList[index];
                    
                    foldout.text = string.IsNullOrEmpty(character.Name)
                        ? "Character"
                        : character.Name;
                    
                    foldout.AddManipulator(CharacterSettings(character));


                    //-- name binding--
                    if (textField.userData is EventCallback<FocusOutEvent> oldName)
                    {
                        textField.UnregisterCallback(oldName);
                        textField.userData = null;
                    }

                    EventCallback<FocusOutEvent> newName = (evt) =>
                    {
                        character.Name = textField.value;
                        foldout.text = string.IsNullOrEmpty(textField.value) ?  "Character" : textField.value; 
                    };
                    
                    textField.RegisterCallback(newName);
                    textField.userData = newName;
                    textField.value = character.Name;

                    // --colour Binding--
                    if (colour.userData is EventCallback<ChangeEvent<Color>> oldColour)
                    {
                        colour.UnregisterValueChangedCallback(oldColour);
                    }

                    colour.value = character.Colour;

                    EventCallback<ChangeEvent<Color>> newColor = (evt) => { character.Colour = evt.newValue; };
                    colour.RegisterValueChangedCallback(newColor);
                    colour.userData = newColor;
                    
                    button.clickable = new Clickable(() => ActivateNode(index));
                }
            }
        };

        if (_selectedSceneData == null)
        {
            _charListView.style.display =  DisplayStyle.None;
            return _charListView;
        }

        _charListView.itemsSource = _selectedSceneData.CharacterList;
        _charListView.style.display =  DisplayStyle.Flex;


        _charListView.selectionChanged += OnSelection;

        return _charListView;
    }

    private IManipulator CharacterSettings(CharacterData character)
    {
        CharacterData targetChar = character;
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("Delete", action => DeleteCharacter(targetChar))
            );
        return contextualMenuManipulator;
    }

    private void DeleteCharacter(CharacterData targetChar)
    {
       _selectedSceneData.CharacterList.Remove(targetChar);
       RefreshCharacter();
    }


    private void AddCharacter()
    {
        if (_selectedSceneData == null) return;

        _selectedSceneData.CharacterList.Add(new CharacterData());

        RefreshCharacter();

        int index = _selectedSceneData.CharacterList.Count - 1;
        _charListView.selectedIndex = index;
        _charListView.ScrollToItem(index);
    }


   


    private void RefreshCharacter()
    {
        if (_selectedSceneData == null || _selectedSceneData.CharacterList == null) return;

        _charListView.itemsSource = _selectedSceneData.CharacterList;
        _charListView.style.display =  DisplayStyle.Flex;
        _charListView.Rebuild();
    }
}
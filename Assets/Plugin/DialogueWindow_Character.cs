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
        charListView = new ListView
        {
            reorderable = true,
            itemsSource = selectedSceneData.CharacterList,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            makeItem = () =>
            {
                var container = new VisualElement
                {
                    style =
                    {
                        height = 100,
                        flexDirection = FlexDirection.Column,
                        backgroundColor = Color.black
                    }
                };
                
                textName = new TextField {
                    value = "Name",
                };
                container.Add(textName);
                colourField = new ColorField
                {
                    value = new Color32(255, 255, 255, 255),
                };
 
                container.Add(colourField);

                var addNode = (new Button()
                {
                    text = "Add Character",
                    style =
                    {
                        height = 25,
                        alignContent = Align.Center
                    }
                });
                container.Add(addNode);
                return container;
            },
            bindItem = (element, index) =>
            {
                var label = element.Q <TextField>();
                var colour = element.Q<ColorField>();
                var button =  element.Q<Button>();
                if (index < selectedSceneData.CharacterList.Count && selectedSceneData.CharacterList[index] != null)
                {
                    var character = selectedSceneData.CharacterList[index];

                    if (label.userData is EventCallback<ChangeEvent<string>> oldName)
                    {
                        label.UnregisterValueChangedCallback(oldName);
                    }
                    label.value = character.Name;
                    
                    EventCallback<ChangeEvent<string>> newName = (evt) =>
                    {
                        character.Name = evt.newValue;
                    };
                    label.RegisterCallback(newName);
                    label.userData = newName;
                    
                    if (colour.userData is EventCallback<ChangeEvent<Color>> oldColour)
                    {
                        colour.UnregisterValueChangedCallback(oldColour);
                    }
                    
                    colour.value = character.Colour;
                    
                    EventCallback<ChangeEvent<Color>> newColor = (evt) =>
                    {
                        character.Colour = evt.newValue;
                    };
                    colour.RegisterCallback(newColor);
                    label.userData = newColor;
                    

                    button.clickable = new Clickable(() => ActivateNode(index));
                }
            }
        };

        charListView.selectionChanged += OnSelection;

        return charListView;
    }

    private void AddCharacter()
    {
        if (selectedSceneData == null) return;

        selectedSceneData.CharacterList.Add(new CharacterData());

        RefreshCharacter();

        int index = selectedSceneData.CharacterList.Count - 1; 
        charListView.selectedIndex  = index;
        charListView.ScrollToItem(index);
        

        //  
        //  Label newCharacter = new Label
        //  {
        //      style =
        //      {
        //          marginTop = 2.5f,
        //          marginLeft = 2.5f,
        //          marginRight = 2.5f,
        //          marginBottom = 2.5f,
        //          
        //          flexDirection = FlexDirection.Column,
        //          backgroundColor = Color.blue,
        //          borderBottomColor = Color.black,
        //          borderBottomWidth = 1, 
        //          
        //          borderTopLeftRadius = 5,
        //          borderTopRightRadius = 5,
        //          borderBottomLeftRadius =  5,
        //          borderBottomRightRadius = 5,
        //      }
        //  };
        //
        //
        //  //List<CharacterData> allCharacters = SceneList.SelectMany(d => d.DialogueList.ToList());    
        //  
        //  textName = new TextField { 
        //      value = "Name",
        //  };
        // newCharacter.Add(textName); 
        // colourField = new ColorField();
        //
        //  newCharacter.Add(colourField);
        //
        //  newCharacter.Add(new Button(ActivateNode)
        //  {
        //      text = "Add Character",
        //      style =
        //      {
        //          height = 25,
        //          alignContent = Align.Center
        //      }
        //  });
        //
        //  charColumn.Add(newCharacter);
    }


    private void ActivateNode(int index)
    {
        Vector2 position = new Vector2(200, 200);
        string characterName = selectedSceneData.CharacterList[index].Name;
        Color colour = selectedSceneData.CharacterList[index].Colour; 
        Debug.Log("characterID" + index + " name " + characterName + " color " + colour);
        
        DialogueNode node = graphView.CreateNode(position, characterName, colour);
        graphView.AddElement(node);
    }


    private void RefreshCharacter()
    {
        if (selectedSceneData == null) return;
        charListView.Clear();
        charListView.itemsSource = selectedSceneData.CharacterList;
        charListView.Rebuild();
    }
}
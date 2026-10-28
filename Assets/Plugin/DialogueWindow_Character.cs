using System;
using System.Collections.Generic;
using System.Linq;
using DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
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

    private void AddCharacter()
    {
        if (selectedSceneData == null) return;
        if (selectedSceneData.CharacterList == null)
        {
            selectedSceneData.CharacterList = new List<CharacterData>();
        }

        var newCharacter = new CharacterData();
    newCharacter.Name = "New Character";
    newCharacter.Colour  = Color.black;
    
    selectedSceneData.CharacterList.Add(newCharacter);
    charListView?.Rebuild();
    
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


    private void ActivateNode()
    {
        Vector2 position = new Vector2(200, 200);

        DialogueNode node = graphView.CreateNode(position, textName.text, colourField.value);
        graphView.AddElement(node);
    }


    private void RefeshCharacter()
    {
        
        if (selectedSceneData == null) return;
    }
}
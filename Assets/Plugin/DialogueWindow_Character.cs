using System;
using System.Collections.Generic;
using DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;


public partial class DialogueWindow
{
    EditorToolbarButton addCharacterButton;

    private VisualElement CharacterRow()
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
    private Color colour;
    private void AddCharacter()
    {
        
        Label newCharacter = new Label
        {
            style =
            {
                marginTop = 2.5f,
                marginLeft = 2.5f,
                marginRight = 2.5f,
                marginBottom = 2.5f,
                
                flexDirection = FlexDirection.Column,
                backgroundColor = Color.blue,
                borderBottomColor = Color.black,
                borderBottomWidth = 1, 
                
                borderTopLeftRadius = 5,
                borderTopRightRadius = 5,
                borderBottomLeftRadius =  5,
                borderBottomRightRadius = 5,
            }
        };
        
   
       newCharacter.Add(new TextField
        { 
            value = "Name",
        });
        
        // Foldout imageFoldout = new Foldout
        // {
        //     text = "Character Image",
        //     style =
        //     {
        //         flexGrow = 1
        //     }
        // };
        // imageFoldout.Add(new ObjectField
        // {
        //     objectType = typeof(Texture2D),
        // });
        //newCharacter.Add(imageFoldout);
        newCharacter.Add(new ColorField());
        newCharacter.Add(new Button(ActivateNode)
        {
            text = "Add Character",
            style =
            {
                height = 25,
                alignContent = Align.Center
            }
        });

        charColumn.Add(newCharacter);
    }
        

    private void ActivateNode()
    {
        if (graphView == null) return;

        DialogueNode newNode = new DialogueNode();
        newNode.name = "Dynamic Node";

        Vector2 spawnPos = graphView.contentViewContainer.WorldToLocal(Vector2.zero);
        newNode.SetPosition(new Rect(spawnPos, new Vector2(200, 150)));


        graphView.AddElement(newNode);
    }
}
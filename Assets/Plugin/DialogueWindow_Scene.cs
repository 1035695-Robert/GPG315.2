using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public partial class DialogueWindow
{
    TextField selectableLabel;
    private VisualElement SceneRow()
    {
        var sceneRow =new VisualElement();
        sceneRow.AddToClassList("column-header");

        sceneRow.Add(new Label
        {
            text = "Add Scene", style =
            {
                flexGrow = 1,
                marginLeft = 4,
                unityTextAlign = TextAnchor.MiddleLeft
            }
        });
        var toolbarButton = new EditorToolbarButton(() => AddScene())
        {
            iconImage = EditorGUIUtility.IconContent("d_Toolbar Plus").image as Texture2D,
        };
        toolbarButton.AddToClassList("header-buttons");
        sceneRow.Add(toolbarButton);
        return sceneRow;
    }

    private void AddScene()
    {
        Debug.Log("Add Scene");
        var textLabel = new Label
        {
            style =
            {
                width = Length.Percent(100),
                height = 25,
            },
        };
       
         selectableLabel = new TextField
        {
            value = "New Scene",
            isReadOnly = true,
            tripleClickSelectsLine = true,
            style =
            {
                flexGrow = 1,
                whiteSpace = WhiteSpace.Normal
            }
        };
        textLabel.Add(selectableLabel);

        VisualElement textInput = selectableLabel.Q("unity-text-input");
        if (textInput != null)
        {
            textInput.style.backgroundColor = Color.clear;

            textInput.style.borderTopWidth = 0;
            textInput.style.borderBottomWidth = 0;
            textInput.style.borderLeftWidth = 0;
            textInput.style.borderRightWidth = 0;

            textInput.style.paddingLeft = 0;
            textInput.style.paddingRight = 0;
            textInput.style.paddingTop = 0;
            textInput.style.paddingBottom = 0;

            textInput.style.cursor = StyleKeyword.Initial;
        }
       
        textLabel.RegisterCallback<ClickEvent>(evt =>
        {
            OnLabelSelect(textLabel, selectableLabel.value );
        });
        sceneColumn.Add(textLabel);
    }

    private Label currentSelection;

    private void OnLabelSelect(Label textLabel, string value) 
    {
        if (currentSelection != null)
        {
            currentSelection.RemoveFromClassList("selected-label");
        }
        addCharacterButton.SetEnabled(currentSelection != null);
        MasterDialogueListData  dialogueListData = SceneList.Find(select => select.DialogueName ==  value);
        //SceneList.Add(textLabel.;)
        textLabel.AddToClassList("selected-label");
        currentSelection = textLabel;
       
        Debug.Log("display this scene");
        //have one selected
    }
}
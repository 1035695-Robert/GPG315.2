using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using PopupWindow = UnityEditor.PopupWindow;

public partial class DialogueWindow
{
    private VisualElement SceneRow()
    {
        var sceneRow = HeaderMarginSet(new VisualElement());


        sceneRow.Add(new Label
        {
            text = "Add Scene", style =
            {
                flexGrow = 1,
                marginLeft = 4,
                unityTextAlign = TextAnchor.MiddleLeft
            }
        });
        sceneRow.Add(AddButton("d_Toolbar Plus"));
        return sceneRow;
    }

    private void AddScene()
    {
        Debug.Log("Add Scene");

        TextField selectableLabel = new TextField
        {
            value = "New Scene",
            isReadOnly = false,
            style =
            {
                whiteSpace = WhiteSpace.Normal
            }
        };


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
       

        sceneColumn.Add(selectableLabel);
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;


public partial class DialogueWindow
{
    private VisualElement CharacterRow()
    {
        var charRow = HeaderMarginSet(new VisualElement());

        charRow.Add(new Label
        {
            text = "Add Character", style =
            {
                flexGrow = 1,
                marginLeft = 4,
                unityTextAlign = TextAnchor.MiddleLeft
            }
        });
            
            charRow.Add(AddButton("d_Toolbar Plus"));
            return charRow;
        }

        private void AddCharacter()
        {
            Debug.Log("New Character");
        }
    }
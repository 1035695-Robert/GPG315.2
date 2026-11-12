using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public partial class DialogueWindow
{
    TextField selectableLabel;
    private bool isCharacterListCreated;

    private VisualElement SceneHeader()
    {
        var sceneRow = new VisualElement();
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

    private ListView CreateSceneList()
    {
        _sceneListView = new ListView
        {
            reorderable = true,

            itemsSource = masterDialogueObject != null ? masterDialogueObject.sceneData  : new List<SceneData>(),
            fixedItemHeight = 24,
            makeItem = () =>
            {
                var label = new Label
                {
                    style = { unityTextAlign = TextAnchor.MiddleLeft }
                };
                return label;
            },
            bindItem = (element, index) =>
            {
                var label = element as Label;
                if (index < masterDialogueObject.sceneData.Count && masterDialogueObject.sceneData[index] != null)
                {
                    label.text = string.IsNullOrEmpty(masterDialogueObject.sceneData[index].SceneName)
                        ? $" new Scene"
                        : masterDialogueObject.sceneData[index].SceneName;
                }
            }
        };

        _sceneListView.selectionChanged += OnSelection;

        return _sceneListView;
    }
    
    
    private void OnSelection(IEnumerable<object> scenes)
    {
        foreach (var scene in scenes)
        {
            var selectedScene = scene as SceneData;
            if (selectedScene != null)
            {
                Debug.Log("selected");
                _selectedSceneData = selectedScene;

                RefreshCharacter();
            }
        }
    }
    
    
    private void AddScene()
    {
        var newScene = new SceneData();
        masterDialogueObject.sceneData.Add(newScene);
        _sceneListView.Rebuild();

        _selectedSceneData = newScene;


        int index = masterDialogueObject.sceneData.Count - 1;
        _sceneListView.SetSelection(new int[] { index });
    }
}
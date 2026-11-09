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
        sceneListView = new ListView
        {
            reorderable = true,

            itemsSource = SceneList,
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
                if (index < SceneList.Count && SceneList[index] != null)
                {
                    label.text = string.IsNullOrEmpty(SceneList[index].SceneName)
                        ? $" scene{index} (No Name)"
                        : SceneList[index].SceneName;
                }
            }

        };

        sceneListView.selectionChanged += OnSelection;

        return sceneListView;
    }

    private void OnSelection(IEnumerable<object> scenes)
    {
        foreach (var scene in scenes)
        {
            var selectedScene = scene as MasterDialogueListData;
            if (selectedScene != null)
            {
                Debug.Log("selected");
                selectedSceneData = selectedScene;

                RefreshCharacter();
            }
        }

    }

    private void AddScene()
    {
        var newScene = CreateInstance<MasterDialogueListData>();
        SceneList.Add(newScene);
        sceneListView.Rebuild();

        selectedSceneData = newScene;


        int index = SceneList.Count - 1;
        sceneListView.SetSelection(new int[] { index });
    }
}
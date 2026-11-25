using System;
using System.Collections.Generic;
using System.Linq;
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

            itemsSource = masterDialogueDataObject != null ? masterDialogueDataObject.sceneData : new List<SceneData>(),
            fixedItemHeight = 24,
            makeItem = () =>
            {
                var label = new Label()
                {
                    style =
                    {
                        unityTextAlign = TextAnchor.MiddleLeft,
                        marginLeft = 4,
                    }
                };
                return label;
            },
            bindItem = (element, index) =>
            {
                var label = element as Label;
                if (index < masterDialogueDataObject.sceneData.Count &&
                    masterDialogueDataObject.sceneData[index] != null)
                {
                    var scene = masterDialogueDataObject.sceneData[index];
                  
                    label.text = string.IsNullOrEmpty(masterDialogueDataObject.sceneData[index].SceneName)
                        ? $"Scene {index+1}"
                        : masterDialogueDataObject.sceneData[index].SceneName;
                }
            }
        };

        _sceneListView.selectionChanged += OnSelection;

        return _sceneListView;
    }
    


    private void OnSelection(IEnumerable<object> scenes)
    {
        var selectedScene = scenes?.OfType<SceneData>().FirstOrDefault();
        if (selectedScene == null)
        {
            _selectedSceneData = null;
            _graphView?.ClearGraph();
            _charListView.style.display = DisplayStyle.None;
            return;
        }

        _selectedSceneData = selectedScene;
        
        RefreshCharacter();
        RefreshGraph();
    }


    private void AddScene()
    {
        if (masterDialogueDataObject == null) return;
        
        var newScene = CreateInstance<SceneData>();
        string defaultName = "Scene" + (masterDialogueDataObject.sceneData.Count + 1);
        newScene.name = defaultName;
        newScene.Initialize(defaultName);
        
        AssetDatabase.AddObjectToAsset(newScene, masterDialogueDataObject);
        masterDialogueDataObject.sceneData.Add(newScene);
        
        
        EditorUtility.SetDirty(masterDialogueDataObject);
        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(masterDialogueDataObject));
        
        _sceneListView.Rebuild();

        _selectedSceneData = newScene;


        int index = masterDialogueDataObject.sceneData.Count - 1;
        _sceneListView.SetSelection(new int[] { index });
    }
}
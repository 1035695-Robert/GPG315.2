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

    private VisualElement SceneHeader()
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
            var selectedScene =  scene as MasterDialogueListData;
            if (selectedScene != null)
            {Debug.Log("selected");
                selectedSceneData = selectedScene;
               
                RefreshCharacter();
            }
        }
            
    }

    private void AddScene()
    {
        SceneList.Add(CreateInstance<MasterDialogueListData>());
        sceneListView.Rebuild();
        
        charColumn.Add(CreateCharacterList());
        
        int index = SceneList.Count - 1;
        sceneListView.SetSelection(new int[] {index});
        

        #region oldAddScene




        //     Debug.Log("Add Scene");
        //     var textLabel = new Label
        //     {
        //         style =
        //         {
        //             width = Length.Percent(100),
        //             height = 25,
        //         },
        //     };
        //    
        //      selectableLabel = new TextField
        //     {
        //         value = "New Scene",
        //         isReadOnly = true,
        //         tripleClickSelectsLine = true,
        //         style =
        //         {
        //             flexGrow = 1,
        //             whiteSpace = WhiteSpace.Normal
        //         }
        //     };
        //     textLabel.Add(selectableLabel);
        //
        //     VisualElement textInput = selectableLabel.Q("unity-text-input");
        //     if (textInput != null)
        //     {
        //         textInput.style.backgroundColor = Color.clear;
        //
        //         textInput.style.borderTopWidth = 0;
        //         textInput.style.borderBottomWidth = 0;
        //         textInput.style.borderLeftWidth = 0;
        //         textInput.style.borderRightWidth = 0;
        //
        //         textInput.style.paddingLeft = 0;
        //         textInput.style.paddingRight = 0;
        //         textInput.style.paddingTop = 0;
        //         textInput.style.paddingBottom = 0;
        //
        //         textInput.style.cursor = StyleKeyword.Initial;
        // }
        //
        // textLabel.RegisterCallback<ClickEvent>(evt =>
        // {
        //     OnLabelSelect(textLabel, selectableLabel.value );
        // });
        // sceneColumn.Add(textLabel);

        #endregion
    }
    #region CalledFromOldAddScene
    //private Label currentSelection;

    // private void OnLabelSelect(Label textLabel, string value) 
    // {
    //     if (currentSelection != null)
    //     {
    //         currentSelection.RemoveFromClassList("selected-label");
    //     }
    //     addCharacterButton.SetEnabled(currentSelection != null);
    //     MasterDialogueListData  dialogueListData = SceneList.Find(select => select.DialogueName ==  value);
    //     //SceneList.Add(textLabel.;)
    //     textLabel.AddToClassList("selected-label");
    //     currentSelection = textLabel;
    //    
    //     Debug.Log("display this scene");
    //     //have one selected
    // }
    #endregion
}
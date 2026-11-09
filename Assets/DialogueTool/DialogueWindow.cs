using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

public partial class DialogueWindow : EditorWindow
{

    VisualElement sceneColumn;
    VisualElement GraphColumn;
    VisualElement charColumn;
    private Toolbar tools;
    
    public ListView sceneListView;
    public ListView charListView;

    private TwoPaneSplitView rightSplitView;
    private VisualElement scene;

    private TwoPaneSplitView leftSplitView;

    DSGraphView graphView;

    public List<MasterDialogueListData> SceneList = new List<MasterDialogueListData>();
    private MasterDialogueListData selectedSceneData;
    
    private List<LanguageData> languageList = new List<LanguageData>();
   
    
    public static void ShowWindow(DialogueAsset asset)
    {
        var window = GetWindow<DialogueWindow>();
        window.titleContent = new GUIContent("Dialogue");
    }
    
    public void CreateGUI()
    {
        StyleSheet sheet = (StyleSheet)EditorGUIUtility.Load("DialogueUIVisualElements.uss");
        rootVisualElement.styleSheets.Add(sheet);

       
        CreateToolBar();
        CreateColumns();
    }

    // private void OnSceneSelected(IEnumerable<object> obj)
    // {
    //     
    // }


    private void CreateToolBar()
        {   tools = new Toolbar();

            rightSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            leftSplitView = new TwoPaneSplitView
            {
                fixedPaneIndex = 0,
                fixedPaneInitialDimension = 100,
                orientation = TwoPaneSplitViewOrientation.Horizontal
            };
            CreateNewLanguageMenuPopup();
            LanguageDropDown();
        }


        private void CreateColumns()
        {
            sceneColumn = new VisualElement();
            GraphColumn = new VisualElement();
            charColumn = new VisualElement();
            
            sceneColumn.Add(SceneHeader());
            sceneColumn.Add(CreateSceneList());
            
            charColumn.Add(CharacterHeader());
            charColumn.Add(CreateCharacterList());

            
            AddGraph();

            rightSplitView.Add(charColumn);
            rightSplitView.Add(GraphColumn);

            leftSplitView.Add(sceneColumn);
            leftSplitView.Add(rightSplitView);

            rootVisualElement.Add(leftSplitView);
            
            rootVisualElement.Add(_popupOverlay);
        }


        private VisualElement HeaderMarginSet(VisualElement element)
        {
            return element;
        }
    }
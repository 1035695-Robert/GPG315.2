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
    public List<string> LanguagesList = new List<string> { "English", "German" };

    private TwoPaneSplitView rightSplitView;
    private VisualElement scene;

    private TwoPaneSplitView leftSplitView;

    DSGraphView graphView;

    public List<MasterDialogueListData> SceneList = new List<MasterDialogueListData>();
    private MasterDialogueListData selectedSceneData;
    public ListView sceneListView;
    public ListView charListView;

    [MenuItem("Window/Dialogue")]
    public static void ShowWindow()
    {
        var window = GetWindow<DialogueWindow>();
        window.titleContent = new GUIContent("Dialogue");
    }


    private void OnEnable()
    {
        tools = new Toolbar();

        rightSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
        leftSplitView = new TwoPaneSplitView
        {
            fixedPaneIndex = 0,
            fixedPaneInitialDimension = 100,
            orientation = TwoPaneSplitViewOrientation.Horizontal
        };
    }

    public void CreateGUI()
    {
        StyleSheet sheet = (StyleSheet)EditorGUIUtility.Load("DialogueUIVisualElements.uss");
        rootVisualElement.styleSheets.Add(sheet);

       
        // sceneListView.selectionChanged += OnSceneSelected;
        CreateToolBar();
        CreateColumns();
    }

    // private void OnSceneSelected(IEnumerable<object> obj)
    // {
    //     
    // }


    private void CreateToolBar()
        {
            var toolbar = new Toolbar();
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

            
            AddGraph();

            rightSplitView.Add(charColumn);
            rightSplitView.Add(GraphColumn);

            leftSplitView.Add(sceneColumn);
            leftSplitView.Add(rightSplitView);

            rootVisualElement.Add(leftSplitView);
        }


        private VisualElement HeaderMarginSet(VisualElement element)
        {
            return element;
        }
    }
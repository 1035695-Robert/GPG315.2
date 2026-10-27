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
    MasterDialogueListData _dialogueListData = new MasterDialogueListData();

    VisualElement sceneColumn;
    VisualElement GraphColumn;
    VisualElement charColumn;
    private Toolbar tools;
    public List<string> LanguagesList = new List<string> { "English", "German" };

    //private MultiColumnListView _multiColumnListView = new MultiColumnListView();
    private TwoPaneSplitView rightSplitView;
    private VisualElement scene;
    private List<string> scenelist;
    private TwoPaneSplitView leftSplitView;
    
    DSGraphView  graphView;
    
   private List<MasterDialogueListData> scenelistData = new List<MasterDialogueListData>();
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
        
        CreateToolBar();
        CreateColumns();
        
    }

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


        sceneColumn.Add(SceneRow());
        charColumn.Add(CharacterRow());
        
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
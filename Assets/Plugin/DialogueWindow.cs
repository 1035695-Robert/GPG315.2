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
    VisualElement popupOverlay;
    private Toolbar tools;
    Label languageCreatePopup;
    public List<string> LanguagesList = new List<string> { "English", "German" };
    TextField newLangaugeName;
    private TwoPaneSplitView rightSplitView;
    private VisualElement scene;

    private TwoPaneSplitView leftSplitView;

    DSGraphView graphView;

    public List<MasterDialogueListData> SceneList = new List<MasterDialogueListData>();
    private MasterDialogueListData masterDialogueListData;
    private SceneData selectedScene;
    private List<SceneData> selectedSceneList = new List<SceneData>();
    public ListView sceneListView;
    public ListView charListView;


    [MenuItem("Window/Dialogue")]
    public static void ShowWindow()
    {
        var window = GetWindow<DialogueWindow>();
        window.titleContent = new GUIContent("Dialogue");
    }

    public void CreateGUI()
    {
        tools = new Toolbar();
        rightSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
        leftSplitView = new TwoPaneSplitView
        {
            fixedPaneIndex = 0,
            fixedPaneInitialDimension = 100,
            orientation = TwoPaneSplitViewOrientation.Horizontal
        };

        StyleSheet sheet = (StyleSheet)EditorGUIUtility.Load("DialogueUIVisualElements.uss");
        rootVisualElement.styleSheets.Add(sheet);

        CreateToolBar();
        
        CreateColumns();
        
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
        rootVisualElement.Add(popupOverlay);
    }


    private VisualElement HeaderMarginSet(VisualElement element)
    {
        return element;
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow : EditorWindow
{
    private VisualElement _sceneColumn;
    private VisualElement _graphColumn;
    private VisualElement _charColumn;
    private Toolbar _tools;

    private ListView _sceneListView;
    private ListView _charListView;

    private TwoPaneSplitView _rightSplitView;
    private VisualElement _scene;

    private TwoPaneSplitView _leftSplitView;

    private DSGraphView _graphView;

    [SerializeField] private MasterDialogue masterDialogueObject;
    private SerializedObject _serializedObject;

    private SceneData _selectedSceneData;


    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        MasterDialogue asset = EditorUtility.InstanceIDToObject(instanceID) as MasterDialogue;
        
        if (asset is MasterDialogue dialogue)
        {
            var window = GetWindow<DialogueWindow>("Dialogue");
            window.ShowWindow(dialogue);

            EditorUtility.SetDirty(asset);
            window.Focus();
            return true;
        }

        return false;
    }
    
    private void ShowWindow(MasterDialogue asset)
    {
        masterDialogueObject = asset;

        if (_sceneListView != null && masterDialogueObject != null)
        {
            _sceneListView.itemsSource = masterDialogueObject.sceneData;
            _sceneListView.RefreshItems();
        }

        DisplayLanguages();
    }


    public void CreateGUI()
    {
        StyleSheet sheet = (StyleSheet)EditorGUIUtility.Load("DialogueUIVisualElements.uss");
        rootVisualElement.styleSheets.Add(sheet);
        _leftSplitView = new TwoPaneSplitView
        {
            fixedPaneIndex = 0,
            fixedPaneInitialDimension = 150,
            orientation = TwoPaneSplitViewOrientation.Horizontal
        };
        _leftSplitView.style.flexGrow = 1;
        _rightSplitView = new TwoPaneSplitView
        {
            fixedPaneIndex = 0,
            fixedPaneInitialDimension = 200,
            orientation = TwoPaneSplitViewOrientation.Horizontal
        };
        _rightSplitView.style.flexGrow = 1;

        CreateToolBar();
        CreateColumns();
    }

    private void CreateToolBar()
    {
        _tools = new Toolbar();

        CreateNewLanguageMenuPopup();
        LanguageDropDown();
    }


    private void CreateColumns()
    {
        _sceneColumn = new VisualElement { style = { minWidth = 150, flexGrow = 1 } };
        _graphColumn = new VisualElement { style = { minWidth = 500, flexGrow = 1 } };
        _charColumn = new VisualElement() { style = { minWidth = 200, flexGrow = 1 } };

        _sceneColumn.Add(SceneHeader());
        _sceneColumn.Add(CreateSceneList());

        _charColumn.Add(CharacterHeader());
        _charColumn.Add(CreateCharacterList());


        AddGraph();
        _leftSplitView.Add(_sceneColumn);

        _rightSplitView.Add(_charColumn);
        _rightSplitView.Add(_graphColumn);

        _leftSplitView.Add(_rightSplitView);

        rootVisualElement.Add(_leftSplitView);

        rootVisualElement.Add(_popupOverlay);
    }
}
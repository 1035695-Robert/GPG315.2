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

    public List<string> LanguagesList = new List<string> { "English", "German" };

    //private MultiColumnListView _multiColumnListView = new MultiColumnListView();
    private TwoPaneSplitView leftSplitView;
    private VisualElement scene;
    private List<string> scenelist;
    private TwoPaneSplitView rightSplitView;

    [MenuItem("Window/Dialogue")]
    public static void ShowWindow()
    {
        var window = GetWindow<DialogueWindow>();
        window.titleContent = new GUIContent("Dialogue");
    }

    private void OnEnable()
    {
        leftSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
        rightSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
        scene = new VisualElement();
    }

    public void CreateGUI()
    {
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

        GraphColumn.Add(GraphRow());

        leftSplitView.Add(charColumn);
        leftSplitView.Add(GraphColumn);

        rightSplitView.Add(sceneColumn);
        rightSplitView.Add(leftSplitView);

        rootVisualElement.Add(rightSplitView);
    }

    private VisualElement AddButton(string icon)
    {
        var toolbarButton = new EditorToolbarButton(() => AddScene())
        {
            iconImage = EditorGUIUtility.IconContent(icon).image as Texture2D,
            style =
            {
                borderLeftColor = Color.clear,
                borderRightColor = Color.clear,
                backgroundColor = Color.clear,
            }
        };
        return toolbarButton;
    }


    //money = add moneyamount
    private VisualElement HeaderMarginSet(VisualElement element)
    {
        var newElement = element;

        newElement.style.unityFontStyleAndWeight = FontStyle.Bold;
        newElement.style.flexDirection = FlexDirection.Row;

        newElement.style.height = 20;
        // newElement.style.marginBottom = 1;
        // newElement.style.marginTop = 1;

        newElement.style.borderBottomWidth = 1;
        newElement.style.borderBottomColor = Color.black;

        return newElement;
    }
}
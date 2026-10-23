using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueWindow : EditorWindow
{
    private class CharacterList
    {
        public string Name;
        public Color Colour;
    }

    private List<CharacterList> characters = new List<CharacterList>
    {
        new CharacterList
        {
            Name = "Character1",
            Colour = Color.white
        }
    };

    [MenuItem("Window/Dialogue")]
    public static void ShowWindow()
    {
        var window = GetWindow<DialogueWindow>();
        window.titleContent = new GUIContent("Dialogue");
    }


    public void CreateGUI()
    {
     TwoPaneSplitView leftSplitView = new TwoPaneSplitView(0,200, TwoPaneSplitViewOrientation.Horizontal);
     leftSplitView.style.flexGrow = 1;
     
     TwoPaneSplitView rightSplitView = new TwoPaneSplitView(0,200, TwoPaneSplitViewOrientation.Horizontal);
     rightSplitView.style.flexGrow = 1;
     
     VisualElement leftColumn =  new VisualElement();
     VisualElement middleColumn = new VisualElement();
     VisualElement rightColumn =  new VisualElement();
    
     
     leftColumn.Add(new Label("Scenes"){style = {unityTextAlign = TextAnchor.MiddleCenter}});
     middleColumn.Add(new Label("Stuff"){style = {unityTextAlign = TextAnchor.MiddleCenter}});
     rightColumn.Add(new Label("Characters"){style = {unityTextAlign = TextAnchor.MiddleCenter}});
     
     leftSplitView.Add(rightColumn);
     leftSplitView.Add(middleColumn);
     
     rightSplitView.Add(leftColumn);
     rightSplitView.Add(leftSplitView);
     
     rootVisualElement.Add(rightSplitView);
    }
}
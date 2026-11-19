using System.Collections.Generic;
using DialogueSystem.Elements;
using DialogueSystem.Enumerators;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public partial class DialogueWindow
{
    private void AddGraph()
    {
        _graphView = new DSGraphView();
        _graphView.name = "graphView";

        _graphColumn.Add(_graphView);

        _graphView.StretchToParentSize();
    }

    private void ActivateNode(int index, DialogueType dialogueType)
    {
        var newDialogue = new DialogueData
        {
            Name = _selectedSceneData.CharacterList[index].Name,
            DialogueText = null,
            Colour = _selectedSceneData.CharacterList[index].Colour,
            Type = dialogueType,
            Position = new Vector2(200, 200)
        };
        _selectedSceneData.DialogueList.Add(newDialogue);
        _graphView.CreateNode(newDialogue);
    }


    void RefreshGraph()
    {
        if (_selectedSceneData == null || _selectedSceneData.DialogueList == null) return;

        _charListView.itemsSource = _selectedSceneData.CharacterList;
        _charListView.style.display = DisplayStyle.Flex;
        _charListView.Rebuild();

        _graphView.ClearGraph();
        
        foreach (DialogueData dialogue in _selectedSceneData.DialogueList)
        {
            
            _graphView.CreateNode(dialogue);
        }
    }
}
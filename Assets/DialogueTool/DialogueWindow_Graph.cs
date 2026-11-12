using System.Collections.Generic;
using DialogueSystem.Elements;
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

    private void ActivateNode(int index)
    {
        Vector2 position = new Vector2(200, 200);
        string characterName = _selectedSceneData.CharacterList[index].Name;
        Color colour = _selectedSceneData.CharacterList[index].Colour;

        var newDialogue = new DialogueData
        {
            Name = characterName,
            dialogueText = null,
            Colour = colour,
            nodePosition = position,
            childNodeIds = new List<string>()
        };
        _selectedSceneData.DialogueList.Add(newDialogue);
        Debug.Log("characterID" + index + " name " + characterName + " color " + colour);

        //DialogueNode node = _graphView.CreateNode(_selectedSceneData);
        DialogueNode node = new DialogueNode();

        node.Initialize(newDialogue);
        node.Draw();
        _graphView.AddElement(node);
    }


    void RefreshGraph()
    {
        if (_selectedSceneData == null || _selectedSceneData.DialogueList == null) return;

        _charListView.itemsSource = _selectedSceneData.CharacterList;
        _charListView.style.display = DisplayStyle.Flex;
        _charListView.Rebuild();
    }
}
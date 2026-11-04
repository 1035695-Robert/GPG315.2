using DialogueSystem.Elements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    
    private void AddGraph()
    {
        graphView = new DSGraphView();
        graphView.name = "graphView";
        GraphColumn.Add(graphView);
        graphView.StretchToParentSize();
    }

    
    private void ActivateNode(int index)
    {
        Debug.Log(index);
        Vector2 position = new Vector2(200, 200);
        string characterName = selectedScene.CharacterList[index].Name;
        Color colour = selectedScene.CharacterList[index].Colour; 
        Debug.Log("characterID" + index + " name " + characterName + " color " + colour);
        selectedScene.DialogueList.Add(new DialogueData
        {
            speakerName = characterName
        });
        DialogueNode node = graphView.CreateNode(position, characterName, colour);
        graphView.AddElement(node);
    }
}
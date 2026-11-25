using DialogueSystem.Elements;
using DialogueSystem.ElementUtility;
using DialogueSystem.Enumerators;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueSingleChoice : DialogueNode
{
  public override void Initialize(DialogueData data, DSGraphView graphView)
  {
    base.Initialize(data, graphView);
    DialogueType = DialogueType.SingleChoice;
   
    if(Choices.Count == 0)
    {
      ChoiceSaveData saveData = new ChoiceSaveData
      {
        Text = "Next Dialogue"
      };
      Choices.Add(saveData);
    }
    
    
  }

  public override void Draw()
  {
    base.Draw();
    foreach (ChoiceSaveData choice in Choices)
    {
      Port choicePort = this.CreatePort(choice.Text);
      choicePort.userData = choice;
      outputContainer.Add(choicePort);
    }
    RefreshExpandedState();
  }
}

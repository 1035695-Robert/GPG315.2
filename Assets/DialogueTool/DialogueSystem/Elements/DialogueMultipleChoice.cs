using DialogueSystem.Elements;
using DialogueSystem.ElementUtility;
using DialogueSystem.Enumerators;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueMultipleChoice : DialogueNode
{
    public override void Initialize(DialogueData data, DSGraphView graphView)
    {
        base.Initialize(data, graphView);
        DialogueType = DialogueType.MultiChoice;

        ChoiceSaveData choiceData = new ChoiceSaveData
        {
            Text = "new Choice"
        };
        Choices.Add(choiceData);
    }

    public override void Draw()
    {
        base.Draw();

        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice", () =>
        {
            Debug.Log("Create");
            ChoiceSaveData choiceData = new ChoiceSaveData()
            {
                Text = "new Choice"
            };
            Choices.Add(choiceData);

            Port choicePort = CreateChoicePort(choiceData);
            outputContainer.Add(choicePort);
        });

        TextContainer.Add(addChoiceButton);

        foreach (ChoiceSaveData choice in Choices)
        {
            Port choicePort = CreateChoicePort(choice);
            outputContainer.Add(choicePort);
        }

        RefreshExpandedState();
    }

    private Port CreateChoicePort(object userdata)
    {
        Port choicePort = this.CreatePort();
        choicePort.userData = userdata;
        ChoiceSaveData choiceData = (ChoiceSaveData)userdata;
        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X", () =>
        {
            if (Choices.Count == 1) return;
            if (choicePort.connected)
            {
                GraphView.DeleteElements(choicePort.connections);
            }

            Choices.Remove(choiceData);
            outputContainer.Remove(choicePort);
        });
        TextField choiceText =
            DialogueElementUtility.CreateTextField(choiceData.Text, evt =>
            {
                choiceData.Text = evt.newValue;
            });
        choicePort.Add(deleteChoiceButton);
        choicePort.Add(choiceText);
        return choicePort;
    }
}
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
}
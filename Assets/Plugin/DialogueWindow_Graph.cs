using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    private VisualElement GraphRow()
    {
        var graphRow = HeaderMarginSet(new VisualElement());

        graphRow.Add(new Label("Stuff")
        {
            text = "Add Scene",
            style =
            {
                marginLeft = 4,
                flexGrow = 1,
               unityTextAlign =  TextAnchor.MiddleLeft
            }
        });

        return graphRow;
    }
    
    
}
using DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DSGraphView : GraphView
{
    public DSGraphView()
    {
        AddManipulators();
        AddGridBackground();
        AddStyle();
    }

    private DialogueNode CreateNode(Vector2 position)
    {
        DialogueNode node = new DialogueNode();
        
        node.Initialize(position);
        node.Draw();
        
       return node;
    }

    private void AddManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        
        this.AddManipulator(CreateNodeContextualMenu());
    }

    private IManipulator CreateNodeContextualMenu()
    {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction("add Node", actionEvent=> AddElement(CreateNode(actionEvent.eventInfo.localMousePosition)))
            );
        return contextualMenuManipulator;
    }

    private void AddStyle()
    {
        StyleSheet sheet = (StyleSheet)EditorGUIUtility.Load("DSGraphViewStyle.uss");
        styleSheets.Add(sheet);
    }

    private void AddGridBackground()
    {
        GridBackground gridBackground = new GridBackground();
        gridBackground.StretchToParentSize();
        Insert(0, gridBackground);
    }
}
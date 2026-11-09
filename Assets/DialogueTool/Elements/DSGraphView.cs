using System.Collections.Generic;
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

    public DialogueNode CreateNode(Vector2 position, string Name, Color colour)
    {
        DialogueNode node = new DialogueNode();
        
        node.Initialize(position, Name, colour);
        node.Draw();
        
       return node;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        ports.ForEach(port =>
        {
            if (startPort == port) return;
            if (startPort.node == port.node) return;
            if (startPort.direction == port.direction) return;
            compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }

    private void AddManipulators()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        
       // this.AddManipulator(CreateNodeContextualMenu());
    }

    // public IManipulator CreateNodeContextualMenu()
    // {
    //     ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
    //         menuEvent => menuEvent.menu.AppendAction("add Node", actionEvent=> AddElement(CreateNode(actionEvent.eventInfo.localMousePosition)))
    //         );
    //     return contextualMenuManipulator;
    // }

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
using System.Collections.Generic;
using DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using DialogueSystem.Enumerators;

public class DSGraphView : GraphView
{
    private MiniMap miniMap;
    public SceneData _currentSceneData;

    public DSGraphView()
    {
        AddManipulators();
        AddGridBackground();
        //AddMiniMap();
        AddStyle();
        //AddMiniMapStyles();
        onGraphVeiwChange();
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
    }

    public void CreateNode(DialogueData data)
    {
        DialogueNode node = data.Type == DialogueType.SingleChoice
            ? new DialogueSingleChoice()
            : new DialogueMultipleChoice();

        node.Initialize(data, this);
        node.Draw();
        AddElement(node);
    }

    private void AddMiniMap()
    {
        miniMap = new MiniMap()
        {
            anchored = true,
        };
        miniMap.SetPosition(new Rect(15, 50, 200, 200));
        Add(miniMap);
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

    // private void AddMiniMapStyles()
    // {
    //     StyleColor backgroundColor = new StyleColor(new Color32(29, 29, 30, 255));
    //     StyleColor borderColour = new StyleColor(new Color32(51, 51, 51, 255));
    //     miniMap.style.backgroundColor = backgroundColor;
    //     miniMap.style.borderBottomColor = borderColour;
    //     miniMap.style.borderTopColor = borderColour;
    //     miniMap.style.borderLeftColor = borderColour;
    //     miniMap.style.borderRightColor = borderColour;
    // }

    public void SetSceneData(SceneData selectedSceneData)
    {
        _currentSceneData = selectedSceneData;
    }

    private void onGraphVeiwChange()
    {
        graphViewChanged = (changes) =>
        {
            Debug.Log("change");
            if (_currentSceneData == null) return changes;
            if (changes.edgesToCreate != null)
            {
                foreach (var edge in changes.edgesToCreate)
                {
                    if (edge.input.node is DialogueNode nextNode && edge.output.userData is ChoiceSaveData choiceData)
                    {
                        choiceData.NodeId = nextNode.ID;
                    }
                }
            }

            if (changes.elementsToRemove != null)
            {
                foreach (GraphElement element in changes.elementsToRemove)
                {
                    if (element is Edge edge && edge.output.userData is ChoiceSaveData choiceData)
                    {
                        choiceData.NodeId = "";
                    }
                    else if (element is DialogueNode node)
                    {
                        _currentSceneData.DialogueList.Remove(node.Data);
                    }
                }
            }

            if (changes.movedElements != null)
            {
                foreach (GraphElement element in changes.movedElements)
                {
                    if (element is DialogueNode node)
                    {
                        node.Data.Position = node.GetPosition().position;
                    }
                }
            }

            EditorUtility.SetDirty(_currentSceneData);
            return changes;
        };
    }

    public void SaveChanges()
    {
        if (_currentSceneData != null)
            EditorUtility.SetDirty(_currentSceneData);
    }

    public void ClearGraph()
    {
        _currentSceneData = null;
        DeleteElements(graphElements.ToList());
    }
}
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueSystem.Elements
{
    using DialogueSystem.Enumerators;
[Serializable]
    public class DialogueNode : Node
    {
        public string DialogueName;
        public List<string> choices;
        public string text;

        public DialogueType dialogueType;

        public void Initialize(Vector2 position)
        {
            DialogueName = "New Dialogue";
            choices = new List<string>();
            text = "add Text.";
            
            SetPosition(new Rect(position, Vector2.zero));
        }

        public void Draw()
        {
            TitleContainer();
            InputContainer();
            OutputContainer();
            TextFieldContainer();
        }


        private void TitleContainer()
        {
            TextField dialogueNameTextField = new TextField
            {
                value = DialogueName
            };
            // Image image = new Image()

            titleContainer.Insert(0, dialogueNameTextField);
        }

        private void InputContainer()
        {
            Port inputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = "Dialogue Connection";
            inputContainer.Add(inputPort);
        }

        private void OutputContainer()
        {
            Port outputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            outputPort.portName = "Dialogue Connection";

            outputContainer.Add(outputPort);
        }

        private void TextFieldContainer()
        {
            VisualElement customContainer = new VisualElement();
            Foldout foldout = new Foldout()
            {
                text = "dialogue text"
            };
            TextField textfield = new TextField() 
            {
                value = text,
                multiline = true,
                style =
                {
                    height = 50,
                }
            };
            foldout.Add(textfield);
            customContainer.Add(foldout);
            extensionContainer.Add(customContainer);
            RefreshExpandedState();
        }
    }
}
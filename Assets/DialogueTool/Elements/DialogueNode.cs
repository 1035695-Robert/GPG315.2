using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueSystem.Elements
{
    using DialogueSystem.Enumerators;
[Serializable]
    public class DialogueNode : Node
    {
        private string CharacterName;
        public List<string> choices;
        public string text;
        public Texture2D profileIcon;
        public DialogueType dialogueType;
        public Color Colour;
StyleSheet styleSheet;
        public Image imageProfile;

        public void Initialize(Vector2 position, string name, Color colour)
        {
            CharacterName =  name;
            Colour = colour;

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
            VisualElement container = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    height = 70,
                    width = 300
                }
            };
           titleContainer.style.height = 70;
           titleContainer.style.width = 300;
            titleContainer.style.backgroundColor = Colour;
            
            // profileIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/test.png");
            // if (profileIcon == null) Debug.LogWarning("couldn't find image profile");
            
            Label imageSlot = new Label
            {
                style =
                { 
                    marginTop = 10,
                    marginBottom = 10,
                    height = 50,
                    width = 50,
                    backgroundColor = Color.black
                }
            };
            container.Add(imageSlot);
            VisualElement textContainer = new VisualElement
            {
                style = {flexGrow = 1}
            };
            TextField NameTextField = new TextField
            {
                value = CharacterName,
                isReadOnly =  true,
                style =
                {
                    marginTop = 10,
                    height = 15,
                }
            };
            textContainer.Add(NameTextField);
            TextField dialogueField = new TextField
            {
                value = CharacterName,
                isReadOnly = true,
                
                style = { height = 30}
            };
            textContainer.Add(dialogueField);
            
            container.Add(textContainer);
            
            titleContainer.Insert(1, container);
        }

        private void InputContainer()
        {
            Port inputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);
        }

        private void OutputContainer()
        {
            Port outputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            outputPort.portName = "Output";

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
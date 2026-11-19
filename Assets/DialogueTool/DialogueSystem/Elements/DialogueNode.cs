using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueSystem.Elements
{
    using Enumerators;
    using ElementUtility;

    [Serializable]
    public class DialogueNode : Node
    {
        public DialogueData Data {get;set;}
        public string ID { get; set; }
        private string CharacterName { get; set; }
        public List<ChoiceSaveData> Choices { get; set; }
        public string Text { get; set; }
        public Texture2D ProfileIcon { get; set; }
        public DialogueType DialogueType { get; set; }
        public Color Colour { get; set; }


        private StyleSheet _styleSheet;
        public Image ImageProfile;
        protected VisualElement TextContainer;
        protected DSGraphView GraphView;

        public virtual void Initialize(DialogueData data, DSGraphView graphView)
        {
            Data = data;
            GraphView ??= graphView;
            ID = string.IsNullOrEmpty(data.ID) ? Guid.NewGuid().ToString() : Data.ID;
            CharacterName = Data.Name;
            Colour = Data.Colour;
            DialogueType = Data.Type;
            Choices = Data.Choices ?? new List<ChoiceSaveData>();
            Text = "add Text.";

            SetPosition(new Rect(Data.Position, Vector2.zero));
        }

        public virtual void Draw()
        {
            TitleContainer();
            InputContainer();
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
            TextContainer = new VisualElement
            {
                style = { flexGrow = 1 }
            };
            var nameTextField = DialogueElementUtility.CreateTextField(CharacterName);

            TextContainer.Add(nameTextField);
            container.Add(TextContainer);

            titleContainer.Insert(1, container);
        }

        private void InputContainer()
        {
            Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input,
                Port.Capacity.Multi);
            inputContainer.Add(inputPort);
        }


        private void TextFieldContainer()
        {
            VisualElement customContainer = new VisualElement();
            Foldout foldout = DialogueElementUtility.CreateFoldout("Dialogue Text");
            TextField textfield = new TextField()
            {
                value = Text,
                multiline = true,
                style =
                {
                    height = 50,
                }
            };
            foldout.Add(textfield);
            customContainer.Add(foldout);
            extensionContainer.Add(customContainer);
        }
    }
}
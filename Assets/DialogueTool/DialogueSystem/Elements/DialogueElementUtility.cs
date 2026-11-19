using System;
using DialogueSystem.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DialogueSystem.ElementUtility
{
    using Elements;
    public static class DialogueElementUtility
    {
        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };
            return button;
        }

        public static Port CreatePort(this DialogueNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
          Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
          port.portName = portName;
          return port;
        }

        public static Foldout CreateFoldout(string title, bool Collapsed = false)
        {
            Foldout foldout = new Foldout
            {
                text = title,
                value = Collapsed
            };
            return foldout;
        }

        public static TextField CreateTextField(string value = null,
            EventCallback<ChangeEvent<string>> onValueChange = null)
        {
            TextField textField = new TextField()
            {
                value = value,
            };
            if (onValueChange != null)
            {
                textField.RegisterValueChangedCallback(onValueChange);
            }

            return textField;
        }

        public static TextField CreateTextArea(string value = null,
            EventCallback<ChangeEvent<string>> onValueChange = null)
        {
            TextField textArea = CreateTextField(value, onValueChange);

            textArea.multiline = true;

            return textArea;
        }
    }
}
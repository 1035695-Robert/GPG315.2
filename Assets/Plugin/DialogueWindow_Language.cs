using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DialogueWindow
{
    private void LanguageDropDown()
    {
        var languageButton = new ToolbarButton
        {
            text = "Language",
            iconImage = EditorGUIUtility.IconContent("d_icon dropdown").image as Texture2D,
            style = { flexDirection = FlexDirection.RowReverse }
        };

        tools.Add(languageButton);
        rootVisualElement.Add(tools);
    }
}

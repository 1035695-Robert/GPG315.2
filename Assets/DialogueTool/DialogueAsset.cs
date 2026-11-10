using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue" , menuName = "Dialogue Asset", order = 1)]
public class DialogueAsset : ScriptableObject
{
  public string dialogueAssetName = "new Dialogue";
  public MasterDialogueListData masterDialogueListData;
  
}
[CustomEditor(typeof(DialogueAsset))]
public class CustomDialogue: Editor
{
  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    
    DialogueAsset dialogue = (DialogueAsset)target;

    GUILayout.Space(10);
    if (GUILayout.Button("Edit Asset window", GUILayout.Height(30)))
      DialogueWindow.ShowWindow(dialogue);
  }
  
}

public class DialogueAssetOpenHandler
{
  [OnOpenAsset(1)]
  public static bool OnOpenAsset(int instanceID, int line)
  {
    Object obj = EditorUtility.InstanceIDToObject(instanceID);
    if (obj is DialogueAsset dialogue)
    {
      DialogueWindow.ShowWindow(dialogue);
      return true;
    }
    return false;
  }
}
using UnityEditor;
using UnityEngine;

public class MyEditorWindow : EditorWindow {
    string windowName = "My Editor Window";
    bool groupEnabled;
    bool DisplayToggle = true;
    float Offset = 1.23f;


    [MenuItem("Window/My Window")]
    public static void ShowWindow() {
        GetWindowWithRect(typeof(MyEditorWindow),
          new Rect(0, 0, 400, 150));
    }


    void OnGUI() {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        windowName = EditorGUILayout.TextField("Window Name",
          windowName);
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        DisplayToggle = EditorGUILayout.Toggle("Display Toggle", DisplayToggle);
        Offset = EditorGUILayout.Slider("Offset Slider",
          Offset, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }
}

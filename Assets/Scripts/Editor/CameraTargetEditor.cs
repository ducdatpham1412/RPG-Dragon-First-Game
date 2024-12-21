using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraLookAt))]
public class CameraTargetEditor : Editor {
    public override void OnInspectorGUI() {
        CameraLookAt targetScript = (CameraLookAt)target;
        targetScript.cameraTarget = EditorGUILayout.Vector3Field("Look At Point",
        targetScript.cameraTarget);
        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }


    void OnSceneGUI() {
        CameraLookAt targetScript = (CameraLookAt)target;
        targetScript.cameraTarget = Handles.PositionHandle(
                targetScript.cameraTarget, Quaternion.identity);
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}

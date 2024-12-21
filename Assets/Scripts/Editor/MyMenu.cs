using UnityEditor;
using UnityEngine;
public class MyMenu {
    [MenuItem("MenuName/MenuItem1")]
    static void EnableMyAwesomeFeature() {
        Debug.Log("I am a leaf on the wind. Watch how I soar.");
    }

    [MenuItem("MenuName/MenuItem1", true)]
    static bool CheckIfAGameObjectIsSelected() {
        return Selection.activeTransform != null;
    }
}
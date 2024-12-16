using System.Collections.Generic;
using UnityEngine;

public class GameState {
    public static Player CurrentPlayer =
             ScriptableObject.CreateInstance<Player>();

    private static Dictionary<string, Vector3> LastScenePositions = new
      Dictionary<string, Vector3>();

    public static bool justExitedBattle;

    public static bool saveLastPosition = true;

    public static Vector3 GetLastScenePosition(string sceneName) {
        if (LastScenePositions.ContainsKey(sceneName)) {
            return LastScenePositions[sceneName];
        }
        return Vector3.zero;
    }

    public static void SetLastScenePosition(string sceneName, Vector3 position) {
        if (LastScenePositions.ContainsKey(sceneName)) {
            LastScenePositions[sceneName] = position;
        }
        else {
            LastScenePositions.Add(sceneName, position);
        }
    }


}

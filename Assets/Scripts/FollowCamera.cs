using UnityEngine;
using System.Collections;
public class FollowCamera : MonoBehaviour {
    public float xOffset = 0f;
    public float yOffset = 0f;
    public Transform player;


    void Awake() {
        // We don't need to add below script because we have drag and drop player in Unity
        // player = GameObject.Find("Player").transform;
        // if (player == null) {
        //     Debug.LogError("Player object not found");
        // }
    }

    void LateUpdate() {
        transform.position = new Vector3(
            player.transform.position.x + xOffset, transform.position.y + yOffset, -10
        );
    }
}
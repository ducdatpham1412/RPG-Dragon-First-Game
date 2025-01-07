using UnityEngine;

public class TestScript : MonoBehaviour {
    void Start() {
        float targetWidth = 2f;
        float targetHeight = 12f;

        float currentHeight = Camera.main.orthographicSize;
        float currentWidth = currentHeight * Camera.main.aspect;

        float targetAspect = targetWidth / targetHeight;
        float cameraAspect = currentWidth / currentHeight;

        if (targetAspect > cameraAspect) {
            Camera.main.orthographicSize = currentHeight * (targetWidth / currentWidth);
        }
        else if (targetAspect < cameraAspect) {
            Camera.main.orthographicSize = targetHeight / 2;
        }

        // spriteRenderer.bounds.size.x
    }
}

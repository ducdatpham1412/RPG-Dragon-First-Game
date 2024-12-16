using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationPrompt : MonoBehaviour {
    public Vector3 startingPosition;

    void OnCollisionEnter2D(Collision2D col) {
        if (NavigationManager.CanNavigate(tag)) {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(tag);
            GameState.saveLastPosition = false;
            GameState.SetLastScenePosition(SceneManager.GetActiveScene().name,
              startingPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (NavigationManager.CanNavigate(tag)) {
            Debug.Log("attempting to exit via " + tag);
            NavigationManager.NavigateTo(tag);
            GameState.saveLastPosition = false;
            GameState.SetLastScenePosition(SceneManager.GetActiveScene().name,
              startingPosition);
        }
    }
}
using UnityEngine;
using System.Collections;
public class NavigationPrompt : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Borders")) {
            Debug.Log("leave town");
        }
    }
}
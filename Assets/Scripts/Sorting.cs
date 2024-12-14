using UnityEngine;
using System.Collections;

public class Sorting : MonoBehaviour {
    public Transform player;

    void Update() {
        if (transform.position.y >= player.transform.position.y) {
            GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
            GetComponents<BoxCollider2D>()[1].enabled = false;
            GetComponents<BoxCollider2D>()[2].enabled = true;
        }
        else if (transform.position.y < player.transform.position.y) {
            GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
            GetComponents<BoxCollider2D>()[1].enabled = true;
            GetComponents<BoxCollider2D>()[2].enabled = false;
        }
    }
}

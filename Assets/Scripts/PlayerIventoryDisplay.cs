using UnityEngine;
using UnityEngine.UI;

public class PlayerIventoryDisplay : MonoBehaviour {
    public Button invPrefab;

    void Start() {
        foreach (var item in GameState.CurrentPlayer.Inventory) {
            Button inventoryChild = Instantiate(invPrefab, Vector3.zero, Quaternion.identity);
            inventoryChild.transform.parent = transform;
            inventoryChild.GetComponent<Image>().sprite = item.itemImage;
        }
    }
}

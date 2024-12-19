using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {
    public InventoryItem Item;
    public ShopManager Manager;
    Image image;
    Text name;


    public void AddShopItem(InventoryItem item) {
        Item = item;
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = item.itemImage;
        name = transform.GetChild(1).GetComponent<Text>();
        name.text = item.itemName;
    }

    public void PurchaseItem() {
        GameState.CurrentPlayer.AddInventoryItem(Item);
    }

    public void ItemSelected() {
        if (Item != null) {
            Manager.SetShopSelectedItem(this);
        }
    }
}

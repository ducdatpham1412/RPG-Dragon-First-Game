using System.Collections.Generic;

public class Player : Entity {
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public string[] Skills;
    public int Money;

    public void AddInventoryItem(InventoryItem item) {
        Inventory.Add(item);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    [SerializeField]
    private Player player;

	// Use this for initialization
	void Start () {
        //player.inventoryList = Populate();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UseItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryType.HealthPotion:
                player.curHealth += 30;
                player.inventoryDisplayed = !player.inventoryDisplayed;
                player.Inventory.SetActive(player.inventoryDisplayed);
                break;
            case InventoryType.Shield:
                break;
            case InventoryType.StrengthPotion:
                break;
            default:
                break;
        }
    }

    void Populate()
    {
        //if (CurrentPlayer.Inventory.Count == 0)
        //    ItemSlot.SetActive(false);
        //else
        //{
        //    ItemSlot.GetComponent<Image>().sprite = GameState.CurrentPlayer.Inventory[0].Sprite;
        //}
        //GameObject newObj;
        //int inventoryCount = GameState.CurrentPlayer.Inventory.Count;
        //for (int i = 1; i < inventoryCount; i++)
        //{
        //    newObj = Instantiate(ItemSlot, transform);
        //    if (GameState.CurrentPlayer.Inventory[i] != null)
        //    {
        //        newObj.GetComponent<Image>().sprite = GameState.CurrentPlayer.Inventory[i].Sprite;
        //    }
        //}
        //return GameState.CurrentPlayer.Inventory.Count;
    }
}

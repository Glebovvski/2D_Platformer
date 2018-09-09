using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform content;

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
                player.inventoryList[item.itemType]--;
                break;
            case InventoryType.Shield:
                break;
            case InventoryType.StrengthPotion:
                break;
            default:
                break;
        }
    }

    public void AddItem(InventoryType item)
    {
        if (player.inventoryList.ContainsKey(item))
        {
            player.inventoryList[item]++;
        }
        else player.inventoryList[item] = 1;
    }

    public void DisplayItems()
    {
        //var content = GameObject.Find("Content").GetComponent<Transform>();
        if (content.transform.childCount == 0)
        {
            for (int i = 0; i < player.inventoryList.Keys.Count; i++)
            {
                InventoryType type = (InventoryType)i;
                string typeString = type.ToString();
                //InventoryItem item;// = new InventoryItem();

                var item = (GameObject)Instantiate(Resources.Load(typeString));
                item.GetComponent<Transform>().parent = content;
                //item.itemCount.text = player.inventoryList[type].ToString();
            }
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

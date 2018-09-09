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

    public void AddItem(InventoryType item)
    {
        GameObject invItem;
        if (player.inventoryList.ContainsKey(item))
        {
            player.inventoryList[item]++;
            //var invItem = player.inventoryList[item];//.GetComponent<InventoryItem>().itemCount.text = player.inventoryList[item].ToString();
            InventoryItem inventoryItem = Resources.FindObjectsOfTypeAll<InventoryItem>()[0];
            inventoryItem.itemCount.text = player.inventoryList[item].ToString();
        }
        else
        {
            player.inventoryList[item] = 1;
            invItem = (GameObject)Instantiate(Resources.Load(item.ToString()));
            invItem.GetComponent<InventoryItem>().itemCount.text = player.inventoryList[item].ToString();
            invItem.GetComponent<Transform>().SetParent(content);
        }
    }

    public void DisplayItems()
    {
        //var content = GameObject.Find("Content").GetComponent<Transform>();
        if (content.transform.childCount != player.inventoryList.Count)
        {
            for (int i = 0; i < player.inventoryList.Keys.Count; i++)
            {
                InventoryType type = (InventoryType)i;
                string typeString = type.ToString();
                //InventoryItem item;// = new InventoryItem();

                //var item = (GameObject)Instantiate(Resources.Load(typeString));
                //item.GetComponent<InventoryItem>().itemCount.text = player.inventoryList[type].ToString();
                //item.GetComponent<Transform>().SetParent(content);
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

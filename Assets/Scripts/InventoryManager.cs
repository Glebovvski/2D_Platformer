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
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AddItem(InventoryType item)
    {
        GameObject invItem;
        if (player.inventoryList.ContainsKey(item))
        {
            if (player.inventoryList[item] != 0)
            {
                player.inventoryList[item]++;
                InventoryItem inventoryItem = Resources.FindObjectsOfTypeAll<InventoryItem>()[0];
                inventoryItem.itemCount.text = player.inventoryList[item].ToString();
            }

        }
        else
        {
            player.inventoryList[item] = 1;
            invItem = (GameObject)Instantiate(Resources.Load(item.ToString()));
            invItem.GetComponent<InventoryItem>().itemCount.text = player.inventoryList[item].ToString();
            invItem.GetComponent<Transform>().SetParent(content);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Manager
{
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("No Inventory Manager Instance");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Transform content;

    public TMPro.TextMeshProUGUI InventoryDescription;

    // Use this for initialization
    void Start()
    {
    }

    public void AddItem(InventoryType item)
    {
        GameObject invItem;
        if (UICanvas.Instance.player.inventoryList.ContainsKey(item))
        {
            if (UICanvas.Instance.player.inventoryList[item] != 0)
            {
                UICanvas.Instance.player.inventoryList[item]++;
                InventoryItem inventoryItem = Resources.FindObjectsOfTypeAll<InventoryItem>().Where(x => x.itemType == item).First();
                inventoryItem.itemCount.text = UICanvas.Instance.player.inventoryList[item].ToString();
                if (UICanvas.Instance.player.inventoryList[item] == 3)
                    DoorController.Instance.OpenDoor();
            }
        }
        else
        {
            UICanvas.Instance.player.inventoryList[item] = 1;
            invItem = (GameObject)Instantiate(Resources.Load(item.ToString()));
            invItem.GetComponent<InventoryItem>().itemCount.text = UICanvas.Instance.player.inventoryList[item].ToString();
            invItem.GetComponent<Transform>().SetParent(content);
        }
    }

    public void UpdateInventory(InventoryType item)
    {
        GameObject invItem;

        InventoryItem checkItem = content.GetComponentsInChildren<InventoryItem>().Where(x => x.itemType == item).FirstOrDefault();
        if (checkItem != null)
        {
            Debug.Log("Adding " + item);
            InventoryItem inventoryItem = Resources.FindObjectsOfTypeAll<InventoryItem>().Where(x => x.itemType == item).First();
            inventoryItem.itemCount.text = UICanvas.Instance.player.inventoryList[item].ToString();
        }
        else
        {
            invItem = (GameObject)Instantiate(Resources.Load(item.ToString()));
            invItem.GetComponent<InventoryItem>().itemCount.text = UICanvas.Instance.player.inventoryList[item].ToString();
            invItem.GetComponent<Transform>().SetParent(content);
        }
    }

    public void RemoveFromInventory(InventoryType item)
    {
        UICanvas.Instance.player.inventoryList[item]--;
        if (UICanvas.Instance.player.inventoryList[item] == 0)
        {
            UICanvas.Instance.player.inventoryList.Remove(item);
            InventoryItem itemToRemove = content.GetComponentsInChildren<InventoryItem>().Where(x => x.itemType == item).FirstOrDefault();
            Destroy(itemToRemove.gameObject);
        }
    }

    public void ItemDescription(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryType.HealthPotion:
                InventoryDescription.text = "Immideately restores 30 points of Player's health";
                break;
            case InventoryType.Shield:
                InventoryDescription.text = "Activates shield around Player which blocks all the enemies attack for 10 seconds";
                break;
            case InventoryType.StrengthPotion:
                InventoryDescription.text = "Increases attack power of the Player for 10 seconds";
                break;
            case InventoryType.Key:
                InventoryDescription.text = "This is a key. If you find all three keys it opens the door. U know... 'Gameplay'";
                break;
            default:
                break;
        }
    }
}

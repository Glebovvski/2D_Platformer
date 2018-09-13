using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    //[SerializeField]
    //private string itemName;
    [SerializeField]
    public InventoryType itemType;
    [SerializeField]
    private float duration;
    [SerializeField]
    public Text itemCount;

    private Player player;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseItem()
    {
        switch (this.itemType)
        {
            case InventoryType.HealthPotion:
                player.curHealth += 30;
                ManageItem();
                break;
            case InventoryType.Shield:
                ManageItem();
                while (duration > 0)
                {
                    player.Shield.SetActive(true);
                    duration -= Time.deltaTime;
                }
                player.Shield.SetActive(false);
                break;
            case InventoryType.StrengthPotion:
                break;
            default:
                break;
        }
    }

    void ManageItem()
    {
        player.inventoryDisplayed = !player.inventoryDisplayed;
        player.Inventory.SetActive(player.inventoryDisplayed);
        player.inventoryList[itemType]--;
        itemCount.text = player.inventoryList[itemType].ToString();
        if (player.inventoryList[itemType] == 0)
        {
            player.inventoryList.Remove(itemType);
            Destroy(this.gameObject);
        }
    }
}

public enum InventoryType
{
    HealthPotion=0,
    Shield,
    StrengthPotion
}

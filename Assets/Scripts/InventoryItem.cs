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

    // Use this for initialization
    void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseItem()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        switch (this.itemType)
        {
            case InventoryType.HealthPotion:
                player.curHealth += 30;
                player.inventoryDisplayed = !player.inventoryDisplayed;
                player.Inventory.SetActive(player.inventoryDisplayed);
                player.inventoryList[itemType]--;
                break;
            case InventoryType.Shield:
                break;
            case InventoryType.StrengthPotion:
                break;
            default:
                break;
        }
    }
}

public enum InventoryType
{
    HealthPotion=0,
    Shield,
    StrengthPotion
}

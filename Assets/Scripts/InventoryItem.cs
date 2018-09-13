using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    //[SerializeField]
    //private string itemName;
    [SerializeField]
    public InventoryType itemType;
    
    public float duration;
    [SerializeField]
    public Text itemCount;

    private Player player;

    float currCountdownValue;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnMouseDown()
    {
        var inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.UseItem(this);
    }

    public void OnMouseEnter()
    {
        var inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.ItemDescription(this);
    }


    public void OnMouseExit()
    {
        var inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        inventory.InventoryDescription.text = string.Empty;
    }
    // Update is called once per frame
    void Update () {
		
	}

    
}

public enum InventoryType
{
    HealthPotion=0,
    Shield,
    StrengthPotion
}

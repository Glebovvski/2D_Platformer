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

    float currCountdownValue;

    // Use this for initialization
    void Start() {
    }

    public void OnMouseDown()
    {
        InventoryManager.Instance.UseItem(this);
    }

    public void OnMouseEnter()
    {
        InventoryManager.Instance.ItemDescription(this);
    }


    public void OnMouseExit()
    {
        InventoryManager.Instance.InventoryDescription.text = string.Empty;
    }
    // Update is called once per frame
    void Update () {
		
	}

    
}

public enum InventoryType
{
    HealthPotion=0,
    Shield,
    StrengthPotion,
    Key
}

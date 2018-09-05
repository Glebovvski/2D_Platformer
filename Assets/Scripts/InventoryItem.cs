using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    [SerializeField]
    private string itemName;
    [SerializeField]
    public InventoryType itemType;
    [SerializeField]
    private float duration;

    // Use this for initialization
    void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum InventoryType
{
    HealthPotion,
    Shield,
    StrengthPotion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    [SerializeField]
    private string itemName;
    [SerializeField]
    private InventoryType itemType;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private float duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

enum InventoryType
{
    HealthPotion,
    Shield,
    StrengthPotion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int price;
    
    //[HideInInspector]
    public Image itemImage;

    public ItemType ItemType;

    [HideInInspector]
    public bool isBlocked;

    [TextArea(3,5)]
    public string description;
    

    public float power;

    public float restore;

    public int levelRequired;

    [HideInInspector]
    public bool isBought;
    
    public GameObject block;

    private void Start()
    {
        if (block != null)
            UpdateItem(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItem(bool blocked)
    {
        isBlocked = blocked;
        block.SetActive(isBlocked);
    }
}

public enum ItemType
{
    HealthPU,
    StrengthPU,
    StaminaPU,
    DexterityPU,
    HealthPotion,
    ShieldPotion,
    StrengthPotion
}

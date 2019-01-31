using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int price;
    
    //[SerializeField]
    private Image itemImage;

    public ItemType ItemType;

    public bool isBlocked;

    [TextArea(3,5)]
    public string description;

    [HideInInspector]
    public float power;

    public float restore;

    public int levelRequired;

    [HideInInspector]
    public bool isBought;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

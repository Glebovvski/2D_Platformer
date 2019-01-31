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

    public bool isBlocked;

    [TextArea(3,5)]
    public string description;
    

    public float power;

    public float restore;

    public int levelRequired;

    [HideInInspector]
    public bool isBought;

    [SerializeField]
    private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        //itemImage = transform.Find("ItemImage").GetComponent<Image>();
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

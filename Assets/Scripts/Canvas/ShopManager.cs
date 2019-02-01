using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Manager
{
    private static ShopManager _instance;

    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Shop Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private GameObject powerUpsContent;

    [SerializeField]
    private GameObject potionsContent;

    [SerializeField]
    private TextMeshProUGUI description;

    [SerializeField]
    private Image selectedImage;

    [SerializeField]
    private TextMeshProUGUI levelRequiredText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Button buyBtn;

    [SerializeField]
    private ShopItem[] healthPus = new ShopItem[5];

    [SerializeField]
    private ShopItem[] strengthPus = new ShopItem[5];

    [SerializeField]
    private ShopItem[] staminaPus = new ShopItem[5];

    [SerializeField]
    private ShopItem[] dexterityPus = new ShopItem[5];

    private ShopItem selectedItem;

    [SerializeField]
    private PlayerController player;

    public void OpenPowerUpContent()
    {
        powerUpsContent.SetActive(true);
        potionsContent.SetActive(false);
    }

    public void OpenPotionsContent()
    {
        potionsContent.SetActive(true);
        powerUpsContent.SetActive(false);
    }

    public void UpdateShop()
    {
        UpdatePowerUpArray(healthPus);
        UpdatePowerUpArray(strengthPus);
        UpdatePowerUpArray(staminaPus);
        UpdatePowerUpArray(dexterityPus);
    }

    void UpdatePowerUpArray(ShopItem[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (i > 0)
            {
                if (!array[i - 1].isBlocked && array[i - 1].isBought && array[i].levelRequired <= PlayerLevelManager.Instance.Level)
                    array[i].UpdateItem(false);
                else array[i].UpdateItem(true);
            }
        }
    }

    public void SelectItem(ShopItem item)
    {
        selectedItem = item;
        description.text = item.description;
        selectedImage.sprite = item.itemImage.sprite;
        selectedImage.color = new Color(1, 1, 1, 1);
        levelRequiredText.text = "LVL " + item.levelRequired;
        if (item.isBlocked || PlayerLevelManager.Instance.Level < item.levelRequired)
            levelRequiredText.color = Color.red;
        else levelRequiredText.color = Color.black;

        priceText.text = item.price + "G";
        if (item.isBlocked || UICanvas.Instance.player.coins < item.price)
            priceText.color = Color.red;
        else priceText.color = Color.black;

        if (item.isBlocked || item.isBought)
            buyBtn.enabled = false;
        else buyBtn.enabled = true;
    }

    public void Buy()
    {
        int index = 0;
        if (selectedItem != null && !selectedItem.isBought)
        {
            switch (selectedItem.ItemType)
            {
                case ItemType.HealthPU:
                    index = Array.FindIndex(healthPus, x => x == selectedItem);
                    if (index == 3)
                        UICanvas.Instance.player.Health -= 10;
                    UICanvas.Instance.player.Health += selectedItem.power;
                    player.ActivateRestoreHealth(selectedItem.restore);
                    ManageMoney();
                    break;
                case ItemType.StrengthPU:
                    index = Array.FindIndex(strengthPus, x => x == selectedItem);
                    if (index > 0)
                    {
                        UICanvas.Instance.player.Damage -= strengthPus[index - 1].power;
                    }
                    UICanvas.Instance.player.Damage += selectedItem.power;
                    ManageMoney();
                    break;
                case ItemType.StaminaPU:
                    break;
                case ItemType.DexterityPU:
                    break;
                case ItemType.HealthPotion:
                    break;
                case ItemType.ShieldPotion:
                    break;
                case ItemType.StrengthPotion:
                    break;
                default:
                    break;
            }
        }
        UpdateShop();
    }

    void ManageMoney()
    {
        selectedItem.isBought = true;
        UICanvas.Instance.player.coins -= selectedItem.price;
        CoinManager.Instance.UpdateCoinsAmount(UICanvas.Instance.player.coins);
        SkillsManager.Instance.UpdateStats();
    }
}

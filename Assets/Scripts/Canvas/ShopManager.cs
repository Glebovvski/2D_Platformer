﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private GameObject errorPanel;

    [SerializeField]
    private TextMeshProUGUI errorText;

    [SerializeField]
    private GameObject plusOnePrefab;

    public Transform plusOnePos;

    private void Start()
    {
        if (GlobalControl.Instance.savedPlayerData.healthPus.Length != 0)// != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.healthPus.Length; i++)
            {
                healthPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.healthPus[i].isBlocked;
                healthPus[i].isBought = GlobalControl.Instance.savedPlayerData.healthPus[i].isBought;
            }
        }
        
        if (GlobalControl.Instance.savedPlayerData.strengthPus.Length != 0)// != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.strengthPus.Length; i++)
            {
                strengthPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.strengthPus[i].isBlocked;
                strengthPus[i].isBought = GlobalControl.Instance.savedPlayerData.strengthPus[i].isBought;
            }
        }
        
        if (GlobalControl.Instance.savedPlayerData.staminaPus.Length != 0)// != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.staminaPus.Length; i++)
            {
                staminaPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.staminaPus[i].isBlocked;
                staminaPus[i].isBought = GlobalControl.Instance.savedPlayerData.staminaPus[i].isBought;
            }
        }
        
        if (GlobalControl.Instance.savedPlayerData.dexterityPus != null && GlobalControl.Instance.savedPlayerData.dexterityPus.Length > 0)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.dexterityPus.Length; i++)
            {
                dexterityPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.dexterityPus[i].isBlocked;
                dexterityPus[i].isBought = GlobalControl.Instance.savedPlayerData.dexterityPus[i].isBought;
            }
        }
        UpdateShop();
    }
    public void OpenPowerUpContent()
    {
        UICanvas.Instance.PlayNavigationSound();
        powerUpsContent.SetActive(true);
        potionsContent.SetActive(false);
    }

    public void OpenPotionsContent()
    {
        UICanvas.Instance.PlayNavigationSound();
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
                else
                    array[i].UpdateItem(true);
            }
        }
    }

    public void SelectItem(ShopItem item)
    {
        UICanvas.Instance.PlayBtnClickSound();
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
    }

    public void Buy()
    {
        if (selectedItem.isBought)
        {
            errorText.text = "You have already acquired this item";
            errorPanel.SetActive(true);
            UICanvas.Instance.PlayInactiveBtnSound();
        }
        else if (selectedItem.price > UICanvas.Instance.player.coins)
        {
            errorText.text = "You don't have enough gold to purchase this item";
            errorPanel.SetActive(true);
            UICanvas.Instance.PlayInactiveBtnSound();
        }
        else if (selectedItem.levelRequired > PlayerLevelManager.Instance.Level)
        {
            errorText.text = "Your level is less than required";
            errorPanel.SetActive(true);
            UICanvas.Instance.PlayInactiveBtnSound();
        }

        else if (selectedItem != null && !selectedItem.isBought)
        {
            GameObject plusOne = GameObject.Instantiate(plusOnePrefab, plusOnePos.position, Quaternion.identity);
            plusOne.transform.SetParent(this.gameObject.transform);
            int index = 0;
            switch (selectedItem.ItemType)
            {
                case ItemType.HealthPU:
                    index = Array.FindIndex(healthPus, x => x == selectedItem);
                    if (index == 3)
                        UICanvas.Instance.player.Health -= 10;
                    UICanvas.Instance.player.Health += selectedItem.power;
                    PlayerStatsManager.Instance.UpdateHealth();
                    player.UpdateRestoreHealthNumber(selectedItem.restore);
                    ManageMoneyPU();
                    break;
                case ItemType.StrengthPU:
                    index = Array.FindIndex(strengthPus, x => x == selectedItem);
                    if (index > 0)
                    {
                        UICanvas.Instance.player.Damage -= strengthPus[index - 1].power;
                    }
                    UICanvas.Instance.player.Damage += selectedItem.power;
                    ManageMoneyPU();
                    break;
                case ItemType.StaminaPU:
                    index = Array.FindIndex(staminaPus, x => x == selectedItem);
                    if (index > 0)
                    {
                        UICanvas.Instance.player.stamina -= staminaPus[index - 1].power;
                    }
                    UICanvas.Instance.player.stamina += selectedItem.power;
                    ManageMoneyPU();
                    break;
                case ItemType.DexterityPU:
                    index = Array.FindIndex(dexterityPus, x => x == selectedItem);
                    if (index > 0)
                    {
                        UICanvas.Instance.player.Dexterity -= dexterityPus[index - 1].power;
                    }
                    UICanvas.Instance.player.Dexterity += selectedItem.power;
                    ManageMoneyPU();
                    break;
                case ItemType.HealthPotion:
                    InventoryManager.Instance.AddItem(InventoryType.HealthPotion);
                    ManageMoney();
                    break;
                case ItemType.ShieldPotion:
                    InventoryManager.Instance.AddItem(InventoryType.Shield);
                    ManageMoney();
                    break;
                case ItemType.StrengthPotion:
                    InventoryManager.Instance.AddItem(InventoryType.StrengthPotion);
                    ManageMoney();
                    break;
                default:
                    break;
            }
            UICanvas.Instance.PlayBtnClickSound();
        }
        UpdateShop();
    }

    void ManageMoneyPU()
    {
        selectedItem.isBought = true;
        ManageMoney();
        SkillsManager.Instance.UpdateStats();
    }

    void ManageMoney()
    {
        UICanvas.Instance.player.coins -= selectedItem.price;
        CoinManager.Instance.UpdateCoinsAmount(UICanvas.Instance.player.coins);
    }

    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
        UICanvas.Instance.PlayBtnClickSound();
    }

    public void OpenShop()
    {
        UICanvas.Instance.player.HUDIsOpen = true;
        SetActive(true);
        UICanvas.Instance.PlayNavigationSound();
    }

    void SavePlayerStatistics()
    {
        if (GlobalControl.Instance.savedPlayerData.healthPus.Length == 0)// == null)
            GlobalControl.Instance.savedPlayerData.healthPus = new PlayerStatistics.ShopItemBasic[healthPus.Length];
        for (int i = 0; i < healthPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.healthPus[i] = new PlayerStatistics.ShopItemBasic()
            {
                isBlocked = healthPus[i].isBlocked,
                isBought = healthPus[i].isBought
            };
            
        }
        //GlobalControl.Instance.savedPlayerData.healthPus = healthPus;

        if (GlobalControl.Instance.savedPlayerData.strengthPus.Length == 0)// == null)
            GlobalControl.Instance.savedPlayerData.strengthPus = new PlayerStatistics.ShopItemBasic[strengthPus.Length];
        for (int i = 0; i < strengthPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.strengthPus[i] = new PlayerStatistics.ShopItemBasic()
            {
                isBlocked = strengthPus[i].isBlocked,
                isBought = strengthPus[i].isBought
            };
        }

        if (GlobalControl.Instance.savedPlayerData.staminaPus.Length == 0)// == null)
            GlobalControl.Instance.savedPlayerData.staminaPus = new PlayerStatistics.ShopItemBasic[staminaPus.Length];
        for (int i = 0; i < staminaPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.staminaPus[i] = new PlayerStatistics.ShopItemBasic()
            {
                isBlocked = staminaPus[i].isBlocked,
                isBought = staminaPus[i].isBought
            };
        }

        if (GlobalControl.Instance.savedPlayerData.dexterityPus.Length == 0)// == null)
            GlobalControl.Instance.savedPlayerData.dexterityPus = new PlayerStatistics.ShopItemBasic[dexterityPus.Length];
        for (int i = 0; i < dexterityPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.dexterityPus[i] = new PlayerStatistics.ShopItemBasic()
            {
                isBlocked = dexterityPus[i].isBlocked,
                isBought = dexterityPus[i].isBought
            };
        }
    }

    public void CloseShop()
    {
        SavePlayerStatistics();
        SetActive(false);
        UICanvas.Instance.player.HUDIsOpen = false;
        UICanvas.Instance.PlayNavigationSound();
    }
}

using System;
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

    //[SerializeField]
    private ShopItem[] healthPus = new ShopItem[5];

    //[SerializeField]
    private ShopItem[] strengthPus = new ShopItem[5];

    //[SerializeField]
    private ShopItem[] staminaPus= new ShopItem[5];

    //[SerializeField]
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
        healthPus = new ShopItem[] {
            GameObject.Find("Health1").GetComponent<ShopItem>(),
            GameObject.Find("Health2").GetComponent<ShopItem>(),
            GameObject.Find("Health3").GetComponent<ShopItem>(),
            GameObject.Find("Health4").GetComponent<ShopItem>(),
            GameObject.Find("Health5").GetComponent<ShopItem>()
        };
        if (GlobalControl.Instance.savedPlayerData.healthPus != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.healthPus.Length; i++)
            {
                healthPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.healthPus[i].isBlocked;
                healthPus[i].isBought = GlobalControl.Instance.savedPlayerData.healthPus[i].isBought;
            }
        }

        strengthPus = new ShopItem[]
        {
            GameObject.Find("Strength1").GetComponent<ShopItem>(),
            GameObject.Find("Strength2").GetComponent<ShopItem>(),
            GameObject.Find("Strength3").GetComponent<ShopItem>(),
            GameObject.Find("Strength4").GetComponent<ShopItem>(),
            GameObject.Find("Strength5").GetComponent<ShopItem>()
        };
        if (GlobalControl.Instance.savedPlayerData.strengthPus != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.strengthPus.Length; i++)
            {
                strengthPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.strengthPus[i].isBlocked;
                strengthPus[i].isBought = GlobalControl.Instance.savedPlayerData.strengthPus[i].isBought;
            }
        }


        staminaPus = new ShopItem[]
        {
            GameObject.Find("Stamina1").GetComponent<ShopItem>(),
            GameObject.Find("Stamina2").GetComponent<ShopItem>(),
            GameObject.Find("Stamina3").GetComponent<ShopItem>(),
            GameObject.Find("Stamina4").GetComponent<ShopItem>(),
            GameObject.Find("Stamina5").GetComponent<ShopItem>()
        };
        if (GlobalControl.Instance.savedPlayerData.staminaPus != null)
        {
            for (int i = 0; i < GlobalControl.Instance.savedPlayerData.staminaPus.Length; i++)
            {
                staminaPus[i].isBlocked = GlobalControl.Instance.savedPlayerData.staminaPus[i].isBlocked;
                staminaPus[i].isBought = GlobalControl.Instance.savedPlayerData.staminaPus[i].isBought;
            }
        }


        dexterityPus = new ShopItem[]
        {
            GameObject.Find("Dexterity1").GetComponent<ShopItem>(),
            GameObject.Find("Dexterity2").GetComponent<ShopItem>(),
            GameObject.Find("Dexterity3").GetComponent<ShopItem>(),
            GameObject.Find("Dexterity4").GetComponent<ShopItem>(),
            GameObject.Find("Dexterity5").GetComponent<ShopItem>()
        };
        if (GlobalControl.Instance.savedPlayerData.dexterityPus != null)
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
                else
                    array[i].UpdateItem(true);
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
    }

    public void Buy()
    {
        if (selectedItem.isBought)
        {
            errorText.text = "You have already acquired this item";
            errorPanel.SetActive(true);
        }
        else if (selectedItem.price > UICanvas.Instance.player.coins)
        {
            errorText.text = "You don't have enough gold to purchase this item";
            errorPanel.SetActive(true);
        }
        else if (selectedItem.levelRequired > PlayerLevelManager.Instance.Level)
        {
            errorText.text = "Your level is less than required";
            errorPanel.SetActive(true);
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
    }

    public void OpenShop()
    {
        SetActive(true);
    }

    public void CloseShop()
    {
        if (GlobalControl.Instance.savedPlayerData.healthPus == null)
            GlobalControl.Instance.savedPlayerData.healthPus = new ShopItem[healthPus.Length];
        for (int i = 0; i < healthPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.healthPus[i] = healthPus[i];
        }
        
        if (GlobalControl.Instance.savedPlayerData.strengthPus == null)
            GlobalControl.Instance.savedPlayerData.strengthPus = new ShopItem[strengthPus.Length];
        for (int i = 0; i < strengthPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.strengthPus[i] = strengthPus[i];
        }
        
        if (GlobalControl.Instance.savedPlayerData.staminaPus == null)
            GlobalControl.Instance.savedPlayerData.staminaPus = new ShopItem[staminaPus.Length];
        for (int i = 0; i < staminaPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.staminaPus[i] = staminaPus[i];
        }
        
        if (GlobalControl.Instance.savedPlayerData.dexterityPus == null)
            GlobalControl.Instance.savedPlayerData.dexterityPus = new ShopItem[dexterityPus.Length];
        for (int i = 0; i < dexterityPus.Length; i++)
        {
            GlobalControl.Instance.savedPlayerData.dexterityPus[i] = dexterityPus[i];
        }
        SetActive(false);
    }
}

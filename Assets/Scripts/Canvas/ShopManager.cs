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
                if (!array[i - 1].isBlocked && array[i].levelRequired <= PlayerLevelManager.Instance.Level)
                    array[i].UpdateItem(false);
                else array[i].UpdateItem(true);
            }
        }
    }

    public void SelectItem(ShopItem item)
    {
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

        if (item.isBlocked)
            buyBtn.enabled = false;
        else buyBtn.enabled = true;
    }
}

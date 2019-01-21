using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("No Inventory Managet Instance");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Player player;

    [SerializeField]
    private Transform content;

    public Text InventoryDescription;

    private float currCountdownValueForShield;
    private float currCountdownValueForStrength;

    [SerializeField]
    private GameObject StrengthDisplay;
    [SerializeField]
    private GameObject ShieldDisplay;

    [SerializeField]
    private GameObject Inventory;

    [SerializeField]
    private GameObject NavigationManager;

    [SerializeField]
    private GameObject SkillsManager;

    private bool inventoryOpened;

    float damage;

    // Use this for initialization
    void Start()
    {
        inventoryOpened = false;
        damage = player.damage;
        StrengthDisplay.SetActive(false);
        ShieldDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenInventory()
    {
        inventoryOpened = !inventoryOpened;
        player.inventoryDisplayed = inventoryOpened;
        player.HUDIsOpen = inventoryOpened;
        if (inventoryOpened)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        Inventory.SetActive(inventoryOpened);
        NavigationManager.SetActive(inventoryOpened);
        SkillsManager.SetActive(false);
    }

    public void AddItem(InventoryType item)
    {
        GameObject invItem;
        if (player.inventoryList.ContainsKey(item))
        {
            if (player.inventoryList[item] != 0)
            {
                player.inventoryList[item]++;
                InventoryItem inventoryItem = Resources.FindObjectsOfTypeAll<InventoryItem>().Where(x => x.itemType == item).First();
                inventoryItem.itemCount.text = player.inventoryList[item].ToString();
            }
        }
        else
        {
            player.inventoryList[item] = 1;
            invItem = (GameObject)Instantiate(Resources.Load(item.ToString()));
            invItem.GetComponent<InventoryItem>().itemCount.text = player.inventoryList[item].ToString();
            invItem.GetComponent<Transform>().SetParent(content);
        }
    }

    public void UseItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryType.HealthPotion:
                player.curHealth += 30;
                ManageItem(item);
                break;
            case InventoryType.Shield:
                ShieldDisplay.SetActive(true);
                player.isShielded = true;
                player.Shield.Play();
                StartCoroutine(StartCountdown(item, item.duration));
                ManageItem(item);
                break;
            case InventoryType.StrengthPotion:
                StrengthDisplay.SetActive(true);
                player.damage *= 1.5f;
                StartCoroutine(StartCountdown(item, item.duration));
                ManageItem(item);
                break;
            default:
                break;
        }
    }

    public void ItemDescription(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryType.HealthPotion:
                InventoryDescription.text = "Immideately restores 30 points of Player's health";
                break;
            case InventoryType.Shield:
                InventoryDescription.text = "Activates shield around Player which blocks all the enemies attack for 10 seconds";
                break;
            case InventoryType.StrengthPotion:
                InventoryDescription.text = "Increases attack power of the Player for 10 seconds";
                break;
            case InventoryType.Key:
                InventoryDescription.text = "This is a key. If you find all three keys it opens the door. U know... 'Gameplay'";
                break;
            default:
                break;
        }
    }

    void ManageItem(InventoryItem item)
    {
        player.inventoryList[item.itemType]--;
        item.itemCount.text = player.inventoryList[item.itemType].ToString();
        if (player.inventoryList[item.itemType] == 0)
        {
            player.inventoryList.Remove(item.itemType);
            Destroy(item.gameObject);
        }
    }

    public IEnumerator StartCountdown(InventoryItem item, float countdownValue = 10)
    {
        Image durStrenght = StrengthDisplay.transform.Find("Duration").GetComponent<Image>();
        Image durShield = ShieldDisplay.transform.Find("Duration").GetComponent<Image>();
        if (item.itemType == InventoryType.Shield)
        {
            currCountdownValueForShield = countdownValue;
            while (currCountdownValueForShield >= 0)
            {
                if (item.itemType == InventoryType.Shield)
                    durShield.fillAmount = currCountdownValueForShield / countdownValue;
                yield return new WaitForSeconds(1.0f);
                currCountdownValueForShield--;

                if (currCountdownValueForShield == 0)
                {
                    player.Shield.Stop();
                    player.isShielded = false;
                    ShieldDisplay.SetActive(false);
                }
            }
        }

        if (item.itemType == InventoryType.StrengthPotion)
        {
            currCountdownValueForStrength = countdownValue;
            while (currCountdownValueForStrength >= 0)
            {
                if (item.itemType == InventoryType.StrengthPotion)
                    durStrenght.fillAmount = currCountdownValueForStrength / countdownValue;
                yield return new WaitForSeconds(1.0f);
                currCountdownValueForStrength--;

                if (currCountdownValueForStrength == 0)
                {
                    player.damage = damage;
                    StrengthDisplay.SetActive(false);
                }
            }
        }


    }
}

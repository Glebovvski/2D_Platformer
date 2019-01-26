using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : Manager {

    private static UICanvas _instance;

    public static UICanvas Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("UICanvas instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        player = FindObjectOfType<Player>();
    }

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public bool inventoryOpened;
    [HideInInspector]
    public bool skillsOpened;
    
    private float currCountdownValueForShield;
    private float currCountdownValueForStrength;

    [SerializeField]
    private GameObject StrengthDisplay;
    [SerializeField]
    private GameObject ShieldDisplay;

    [SerializeField]
    private Image durStrenght;

    [SerializeField]
    private Image durShield;

    float damage;

    private void Start()
    {
        InventoryManager.Instance.SetActive(false);
        SkillsManager.Instance.SetActive(false);
        NavigationManager.Instance.SetActive(false);
        inventoryOpened = false;
        skillsOpened = false;
        damage = player.Damage;
        StrengthDisplay.SetActive(false);
        ShieldDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpened = !inventoryOpened;
            skillsOpened = false;
            InventoryManager.Instance.SetActive(inventoryOpened);
            NavigationManager.Instance.SetActive(inventoryOpened);
            SkillsManager.Instance.SetActive(skillsOpened);
            Opened();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            skillsOpened = !skillsOpened;
            inventoryOpened = false;
            SkillsManager.Instance.SetActive(skillsOpened);
            NavigationManager.Instance.SetActive(skillsOpened);
            InventoryManager.Instance.SetActive(inventoryOpened);
            Opened();
        }
    }

    void Opened()
    {
        if (inventoryOpened || skillsOpened)
        {
            player.HUDIsOpen = true;
            Time.timeScale = 0;
        }
        else if(!inventoryOpened && !skillsOpened)
        {
            player.HUDIsOpen = false;
            Time.timeScale = 1;
        }
    }

    void ManageItem(InventoryItem item)
    {
        UICanvas.Instance.player.inventoryList[item.itemType]--;
        item.itemCount.text = UICanvas.Instance.player.inventoryList[item.itemType].ToString();
        if (UICanvas.Instance.player.inventoryList[item.itemType] == 0)
        {
            UICanvas.Instance.player.inventoryList.Remove(item.itemType);
            Destroy(item.gameObject);
        }
    }

    public void UseItem(InventoryItem item)
    {
        switch (item.itemType)
        {
            case InventoryType.HealthPotion:
                UICanvas.Instance.player.curHealth += 30;
                ManageItem(item);
                break;
            case InventoryType.Shield:
                ShieldDisplay.SetActive(true);
                UICanvas.Instance.player.isShielded = true;
                UICanvas.Instance.player.Shield.Play();
                StartCoroutine(StartCountdown(item, item.duration));
                ManageItem(item);
                break;
            case InventoryType.StrengthPotion:
                damage = UICanvas.Instance.player.Damage;
                StrengthDisplay.SetActive(true);
                UICanvas.Instance.player.Damage *= 1.5f;
                StartCoroutine(StartCountdown(item, item.duration));
                ManageItem(item);
                break;
            default:
                break;
        }
    }

    public IEnumerator StartCountdown(InventoryItem item, float countdownValue = 10)
    {
        //Image durStrenght = StrengthDisplay.transform.Find("Duration").GetComponent<Image>();
        //Image durShield = ShieldDisplay.transform.Find("Duration").GetComponent<Image>();
        if (item.itemType == InventoryType.Shield)
        {
            currCountdownValueForShield = countdownValue;
            while (currCountdownValueForShield >= 0)
            {
                if (item.itemType == InventoryType.Shield)
                    durShield.fillAmount = currCountdownValueForShield / countdownValue;

                yield return new WaitForSeconds(1.0f);
                Debug.Log("Curr countdown value for shield: " + currCountdownValueForShield);
                currCountdownValueForShield--;
                Debug.Log("Curr countdown value for shield: " + currCountdownValueForShield);
                if (currCountdownValueForShield == 0)
                {
                    UICanvas.Instance.player.Shield.Stop();
                    UICanvas.Instance.player.isShielded = false;
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
                    UICanvas.Instance.player.Damage = damage;
                    StrengthDisplay.SetActive(false);
                }
            }
        }


    }
}

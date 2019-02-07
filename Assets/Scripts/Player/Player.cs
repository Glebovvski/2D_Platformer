using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float Health { get; set; }
    [HideInInspector]
    public float curHealth;

    public bool isShielded;

    
    [HideInInspector]
    public float stamina;
    [HideInInspector]
    public float curStamina;

    [HideInInspector]
    public float Damage { get; set; }

    [HideInInspector]
    public float Dexterity { get; set; }

    [HideInInspector]
    public Dictionary<InventoryType, int> inventoryList;

    public ParticleSystem Shield;

    public AudioClip clip;

    private AudioSource audioSource;

    public bool HUDIsOpen;

    //[HideInInspector] TESTING. SHOULD BE PRIVATE
    public int coins;

    private Animator animator;

    private PlayerController playerController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        isShielded = false;
    }

    // Use this for initialization
    void Start ()
    {
        curHealth = GlobalControl.Instance.savedPlayerData.currentHealth;
        PlayerLevelManager.Instance.Level = GlobalControl.Instance.savedPlayerData.currentLevel;
        PlayerLevelManager.Instance.SkillPoints = GlobalControl.Instance.savedPlayerData.currentSkillPoints;
        PlayerLevelManager.Instance.XP = GlobalControl.Instance.savedPlayerData.currentXP;
        Dexterity = GlobalControl.Instance.savedPlayerData.Dexterity;
        Health = GlobalControl.Instance.savedPlayerData.Health;
        inventoryList = GlobalControl.Instance.savedPlayerData.inventoryList;
        stamina = GlobalControl.Instance.savedPlayerData.Stamina;
        Damage = GlobalControl.Instance.savedPlayerData.Strength;
        coins = GlobalControl.Instance.savedPlayerData.coins;
        CoinManager.Instance.UpdateCoinsAmount(coins);
        playerController.restore = GlobalControl.Instance.savedPlayerData.restore;
        FillInInventory();
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    public void FillInInventory()
    {
        foreach (var item in inventoryList)
        {
            InventoryManager.Instance.UpdateInventory(item.Key);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!Missed())
        {
            animator.SetBool("GotHit", true);
            animator.SetBool("Fight", true);
            curHealth -= amount;
            PlayerStatsManager.Instance.UpdateHealth();
        }
        else Debug.Log("Missed");
        if (curHealth <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Animator>().SetBool("PlayerSpotted", false);
            }
        }
    }

    private bool Missed()
    {
        return (Random.Range(-10, 10) * Dexterity) > Random.Range(0, 10);
    }

    public void AddCoin(int value)
    {
        audioSource.PlayOneShot(clip);
        coins += value;
        CoinManager.Instance.UpdateCoinsAmount(coins);
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.savedPlayerData.currentHealth = curHealth;
        GlobalControl.Instance.savedPlayerData.currentLevel = PlayerLevelManager.Instance.Level;
        GlobalControl.Instance.savedPlayerData.currentSkillPoints = PlayerLevelManager.Instance.SkillPoints;
        GlobalControl.Instance.savedPlayerData.currentXP = PlayerLevelManager.Instance.XP;
        GlobalControl.Instance.savedPlayerData.Dexterity = Dexterity;
        GlobalControl.Instance.savedPlayerData.Health = Health;
        GlobalControl.Instance.savedPlayerData.inventoryList = inventoryList;
        GlobalControl.Instance.savedPlayerData.Stamina = stamina;
        GlobalControl.Instance.savedPlayerData.Strength = Damage;
        GlobalControl.Instance.savedPlayerData.coins = coins;
        GlobalControl.Instance.savedPlayerData.restore = playerController.restore;
    }
}

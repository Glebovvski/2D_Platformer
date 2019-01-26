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

    public bool HUDIsOpen;

    private int coins = 0;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isShielded = false;
        inventoryList = new Dictionary<InventoryType, int>();

        Health = 100;
        stamina = 100;
        Damage = 15;
        Dexterity = 0.5f;
        curHealth = Health;
        curStamina = stamina;
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
        

    }

    public void TakeDamage(int amount)
    {
        animator.SetBool("GotHit", true);
        curHealth -= amount;
        PlayerStatsManager.Instance.healthSlider.value = curHealth;
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

    public void AddCoin(int value)
    {
        coins += value;
        CoinManager.Instance.UpdateCoinsAmount(coins);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private Slider healthSlider;
    private int health;
    [HideInInspector]
    public int curHealth;

    [SerializeField]
    private Slider staminaSlider;
    [HideInInspector]
    public int stamina;
    [HideInInspector]
    public int curStamina;

    public int damage = 1;

    [HideInInspector]
    public Dictionary<InventoryType, int> inventoryList;

    [SerializeField]
    public GameObject Inventory;
    public bool inventoryDisplayed;
    

	// Use this for initialization
	void Start () {
        inventoryList = new Dictionary<InventoryType, int>();
        inventoryDisplayed = false;
        Inventory.SetActive(inventoryDisplayed);
        health = 100;
        stamina = 100;
        curHealth = health;
        curStamina = stamina;
        healthSlider.maxValue = health;
        healthSlider.value = curHealth;
        staminaSlider.maxValue = stamina;
        staminaSlider.value = curStamina;
	}
	
	// Update is called once per frame
	void Update () {
        staminaSlider.value = curStamina;
        healthSlider.value = curHealth;
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryDisplayed = !inventoryDisplayed;
            Inventory.SetActive(inventoryDisplayed);
        }
    }

    public void TakeDamage(int amount)
    {
        GetComponent<Animator>().SetBool("GotHit", true);
        curHealth -= amount;
        healthSlider.value = curHealth;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private Slider healthSlider;
    [HideInInspector]
    public float health { get; set; }
    [HideInInspector]
    public float curHealth;

    public bool isShielded;

    [SerializeField]
    private Slider staminaSlider;
    [HideInInspector]
    public float stamina;
    [HideInInspector]
    public float curStamina;

    [HideInInspector]
    public float damage { get; set; }

    [HideInInspector]
    public float dexterity { get; set; }

    [HideInInspector]
    public Dictionary<InventoryType, int> inventoryList;

    [SerializeField]
    public GameObject Inventory;
    [SerializeField]
    private GameObject Skills;
    
    public InventoryManager InventoryManager;
    public bool inventoryDisplayed;
    public bool skillsDisplayed;

    public ParticleSystem Shield;

	// Use this for initialization
	void Start () {
        isShielded = false;
        inventoryList = new Dictionary<InventoryType, int>();
        inventoryDisplayed = false;
        Inventory.SetActive(inventoryDisplayed);
        skillsDisplayed = false;
        Skills.SetActive(skillsDisplayed);
        health = 100;
        stamina = 100;
        damage = 15;
        dexterity = 0.5f;
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
            if (inventoryDisplayed)
                Time.timeScale = 0;
            Inventory.SetActive(inventoryDisplayed);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            skillsDisplayed = !skillsDisplayed;
            if (skillsDisplayed)
                Time.timeScale = 0;
            Skills.SetActive(skillsDisplayed);
        }
        if (!inventoryDisplayed && !skillsDisplayed)
            Time.timeScale = 1;
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

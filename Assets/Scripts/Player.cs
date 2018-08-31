using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private Slider healthSlider;
    private int health;
    public int curHealth;

    [SerializeField]
    private Slider staminaSlider;
    public int stamina;
    public int curStamina;

    public int damage = 1;

	// Use this for initialization
	void Start () {
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
	}

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        healthSlider.value = curHealth;
        if (curHealth <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
        }

    }
}

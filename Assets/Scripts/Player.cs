using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private Slider healthSlider;
    private int health;
    private int curHealth;

    [SerializeField]
    private Slider staminaSlider;
    private int stamina;
    private int curStamina;

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
		
	}

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        healthSlider.value = curHealth;
        if (curHealth <= 0)
        {

        }

    }
}

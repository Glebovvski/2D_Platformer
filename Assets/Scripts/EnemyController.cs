using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    private Animator animator;

    public float maxHealth = 100f;
    private float curHealth;

    public Image healthBar;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (maxHealth <= 0)
        {
            animator.SetBool("Dead", true);
        }
        healthBar.fillAmount = curHealth / maxHealth;
    }

    public void TakeDamage(int amount)
    {
        animator.SetBool("TakeDamage", true);
        curHealth -= amount;
        healthBar.fillAmount = curHealth / maxHealth;
    }
}

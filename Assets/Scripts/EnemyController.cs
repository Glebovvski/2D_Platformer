using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    private Animator animator;

    public float maxHealth = 100f;
    private float curHealth;

    private float speed = 1;

    private int damage = 5;

    public Image healthBar;

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Canvas canvas;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (curHealth <= 0)
        {
            animator.SetBool("PlayerSpotted", false);
            animator.SetBool("Dead", true);
        }
        healthBar.fillAmount = curHealth / maxHealth;

        if (Vector2.Distance(player.transform.position, transform.position) > 0.4 && player.transform.position.y*2 > transform.position.y)
            animator.SetBool("PlayerSpotted", false);

        if (animator.GetBool("PlayerSpotted"))
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            if (Vector2.Distance(player.transform.position, transform.position) > 0.2)
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    public void TakeDamage(int amount)
    {
        animator.SetBool("TakeDamage", true);
        curHealth -= amount;
        healthBar.fillAmount = curHealth / maxHealth;
    }

    public void Attack()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 0.2)
        {
            if (player.player.isShielded)
                player.player.TakeDamage(0);
            else
                player.player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("PlayerSpotted", true);
            float distance = Vector2.Distance(collision.transform.position, transform.position);
            animator.SetFloat("Distance", distance);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("PlayerSpotted", true);
            float distance = Vector2.Distance(collision.transform.position, transform.position);
            animator.SetFloat("Distance", distance);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("PlayerSpotted", false);
            player.enemies.Remove(this.gameObject);
            float distance = Vector2.Distance(collision.transform.position, transform.position);
            animator.SetFloat("Distance", distance);
        }
    }
}

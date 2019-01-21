using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IEnemy {

    private Animator animator;

    private SpriteRenderer _renderer;

    [HideInInspector]
    public bool IsStopped = false;

    public float maxHealth = 100f;
    private float curHealth;

    private float speed = 1;

    private int damage = 5;

    public Image healthBar;

    [HideInInspector]
    public int Experience = 15;

    [HideInInspector]
    public PlayerController player;

    [SerializeField]
    private Canvas canvas;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        curHealth = maxHealth;
	}

    // Update is called once per frame
    void Update() {
        if (curHealth <= 0)
        {
            animator.SetBool("PlayerSpotted", false);
            animator.SetBool("Dead", true);
        }
        healthBar.fillAmount = curHealth / maxHealth;

        if (!IsStopped)
        {
            if (animator.GetBool("PlayerSpotted"))
            {
                if (player.transform.position.x > transform.position.x)
                    _renderer.flipX = false;
                else _renderer.flipX = true;

                if (Vector2.Distance(transform.position, player.transform.position) > 0.5f)
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("PlayerSpotted", false);
        }
    }

    public void Damage(float amount)
    {
        animator.SetBool("TakeDamage", true);
        curHealth -= amount;
        healthBar.fillAmount = curHealth / maxHealth;
    }

    public void Attack()
    {
        if (!IsStopped)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 0.6f) //0.2
            {
                if (!player.player.isShielded)
                    player.player.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsStopped)
        {
            if (collision.name == "Player")
            {
                animator.SetBool("PlayerSpotted", true);
                float distance = Vector2.Distance(collision.transform.position, transform.position);
                animator.SetFloat("Distance", distance);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsStopped)
        {
            if (collision.name == "Player")
            {
                float distance = Vector2.Distance(collision.transform.position, transform.position);
                animator.SetFloat("Distance", distance);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            animator.SetBool("PlayerSpotted", false);
            player.enemies.Remove(this.gameObject);
            float distance = Vector2.Distance(collision.transform.position, transform.position);
            animator.SetFloat("Distance", distance);
        }
    }

    public void Stop()
    {
        IsStopped = !IsStopped;
    }
}

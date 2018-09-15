using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 150.0f;
    private Vector2 jumpSpeed = new Vector2(0, 4.0f);
    private Rigidbody2D rigidbody2d;
    bool facingRight = true;
    [HideInInspector]
    public Animator animator;
    bool isJumping = false;

    private AudioSource audio;

    public List<GameObject> enemies;

    [SerializeField]
    public Player player;

    public bool isFighting = false;
    private bool isInRange = false;
    bool grounded;
    public int clicks = 0;
    public LayerMask groundLayer;

    // Use this for initialization
    void Start()
    {
        audio = player.GetComponent<AudioSource>();
        enemies = new List<GameObject>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isJumping = !isGrounded();
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));
        //if (!isFighting)
            rigidbody2d.velocity = new Vector2(move * speed * Time.deltaTime, rigidbody2d.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("MyJump") && !isJumping)
        {
            Jump();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (player.curStamina > 10)
            {
                animator.SetTrigger("Clicked");
                animator.SetBool("Fight", true);
                Hit();
            }
        }

        if (!animator.GetBool("Fight"))
        {
            if (player.curStamina < player.stamina)
                player.curStamina += 1;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            animator.SetBool("Jump", true);
            rigidbody2d.AddForce(jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Ground")
            {
                animator.SetBool("Jump", false);
                return true;
            }
        }

        return false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        GetComponent<BoxCollider2D>().transform.localScale = theScale;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Remove(collision.gameObject);
        }
        animator.SetBool("Fight", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            transform.parent = collision.transform;
        }
        if (collision.tag == "Enemy")
        {
            animator.SetBool("Fight", true);
            if (!enemies.Contains(collision.GetComponent<EnemyController>().gameObject))
                enemies.Add(collision.GetComponent<EnemyController>().gameObject);
        }
    }

    private void Hit()
    {
        if (!player.inventoryDisplayed)
        {
            clicks++;
            if (clicks == 1)
            {
                player.curStamina -= 10;
                audio.clip = Resources.Load("Sword1") as AudioClip;
                audio.PlayOneShot(audio.clip);

            }
            if (clicks == 2)
            {
                player.curStamina -= 10;
                animator.SetTrigger("secondAttack");
                Vector2 forward = facingRight ? new Vector2(0.5f, 0) : new Vector2(-1, 0);
                rigidbody2d.velocity = forward * speed * Time.deltaTime;
                audio.clip = Resources.Load("Sword2") as AudioClip;
                audio.PlayOneShot(audio.clip);
            }
            if (clicks == 3)
            {
                player.curStamina -= 10;
                animator.SetTrigger("thirdAttack");
                audio.clip = Resources.Load("Sword1") as AudioClip;
                audio.PlayOneShot(audio.clip);
                clicks = 0;
                //animator.ResetTrigger("Clicked");
            }
        }
    }

    public void Attack()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(player.damage);
        }
    }
}

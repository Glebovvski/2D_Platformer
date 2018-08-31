﻿using System.Collections;
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

    
    public GameObject enemy;

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
        enemy = null;
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
        if (!isFighting)
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
        float distance = 0.5f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            animator.SetBool("Jump", false);
            return true;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = null;
        }
        animator.SetBool("Fight", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetBool("Fight", true);
            if (enemy == null)
                enemy = collision.GetComponent<EnemyController>().gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("Enemy"))
        //{
        //    if (enemy != collision.GetComponent<EnemyController>().gameObject)
        //        enemy = collision.GetComponent<EnemyController>().gameObject;
        //}
    }


    private void Hit()
    {
        clicks++;
        if (clicks == 1)
        {
            player.curStamina -= 10;
            //animator.SetBool("Fight", true);
            //Attack();
        }
        if (clicks == 2)
        {
            player.curStamina -= 10;
            animator.SetTrigger("secondAttack");
            Vector2 forward = facingRight ? new Vector2(0.5f, 0) : new Vector2(-1, 0);
            rigidbody2d.velocity = forward * speed * Time.deltaTime;
            //Attack();
        }
        if (clicks == 3)
        {
            player.curStamina -= 10;
            animator.SetTrigger("thirdAttack");
            //Attack();
            clicks = 0;
        }
    }

    public void Attack()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1f);
        //if(hit.collider!=null)
        //{
        //    if (hit.collider.tag=="Enemy")
        //        hit.collider.GetComponent<EnemyController>().TakeDamage(player.damage);
        //}
        if (enemy != null)
            enemy.GetComponent<EnemyController>().TakeDamage(player.damage);
    }
}

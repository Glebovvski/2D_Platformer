using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 150.0f;
    private Vector2 jumpSpeed = new Vector2(0,4.0f);
    private Rigidbody2D rigidbody2d;
    bool facingRight = true;
    [SerializeField]
    private Transform positionAfterHit;
    Animator animator;
    bool isJumping = false;

    [SerializeField]
    Player player;

    public bool isFighting = false;
    bool grounded;
    public int clicks = 0;

	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.freezeRotation = true;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));
        if (!isFighting)
            rigidbody2d.velocity = new Vector2(move * speed * Time.deltaTime, rigidbody2d.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("MyJump") && !isJumping) {
            isJumping = true;
            animator.SetBool("Jump", isJumping);
            rigidbody2d.AddForce(jumpSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (player.curStamina > 10)
            {
                isFighting = true;
                Hit();
            }
        }

        if (!isFighting)
        {
            if (player.curStamina < player.stamina)
                player.curStamina += 1;
        }
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
        animator.SetBool("Jump", isJumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isJumping)
        {
            isJumping = false;
            animator.SetBool("Jump", isJumping);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isJumping)
        {
            isJumping = false;
            animator.SetBool("Jump", isJumping);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Hit()
    {
        if (isFighting)
        {
            clicks++;
            if (clicks == 1)
            {
                player.curStamina -= 10;
                animator.SetBool("Fight", true);
            }
            if (clicks == 2)
            {
                player.curStamina -= 10;
                animator.SetTrigger("secondAttack");
                Vector2 forward = facingRight ? new Vector2(0.5f, 0) : new Vector2(-1, 0);
                rigidbody2d.velocity = forward * speed*Time.deltaTime;
            }
            if (clicks == 3)
            {
                player.curStamina -= 10;
                animator.SetTrigger("thirdAttack");
                clicks = 0;
            }
            //animator.SetBool("Fight", false);
            //isFighting = false;
        }
    }
}

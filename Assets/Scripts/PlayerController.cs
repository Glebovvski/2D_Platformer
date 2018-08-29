using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 150.0f;
    private Vector2 jumpSpeed = new Vector2(0,4.0f);
    private float gravity = 20f;
    private Rigidbody2D rigidbody2d;
    bool facingRight = true;

    Animator animator;
    bool isJumping = false;
    bool grounded;

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
        rigidbody2d.velocity = new Vector2(move * speed*Time.deltaTime, rigidbody2d.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("MyJump") && !isJumping) {
            isJumping = true;
            animator.SetBool("Jump", isJumping);
            rigidbody2d.AddForce(jumpSpeed, ForceMode2D.Impulse);
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
}

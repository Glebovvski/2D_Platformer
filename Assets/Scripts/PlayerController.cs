using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 4.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20f;
    private Rigidbody2D rigidbody2d;
    bool facingRight = true;

    Animator animator;

	// Use this for initialization
	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.freezeRotation = true;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2d.velocity = new Vector2(move * speed, rigidbody2d.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

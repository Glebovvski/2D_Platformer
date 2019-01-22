using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Talker
{
    [HideInInspector]
    public PlayerLevel PlayerLevel { get; set; }
    private float speed = 200.0f;
    private Vector2 jumpSpeedVector = new Vector2(0, 7.0f); //4
    private Rigidbody2D rigidbody2d;
    bool facingRight = true;
    [HideInInspector]
    public Animator animator;
    bool isJumping = false;

    //[SerializeField]
    //public Text bubbleText;

    private AudioSource audioSource;

    public List<GameObject> enemies;
    
    public Player player;

    public bool isFighting = false;
    bool grounded;

    public int clicks = 0;

    public LayerMask groundLayer;
    

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        PlayerLevel = GetComponent<PlayerLevel>();
        audioSource = player.GetComponent<AudioSource>();
        enemies = new List<GameObject>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.freezeRotation = true;
        animator = GetComponent<Animator>();
        //bubbleCanvas = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        isJumping = !isGrounded();
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2d.velocity = new Vector2(move * speed * Time.deltaTime, rigidbody2d.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("MyJump") && !isJumping)
        {
            clicks = 0;
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (player.curStamina > 10 && !player.HUDIsOpen)
            {
                Debug.Log("HUD is Open: " + player.HUDIsOpen);
                animator.SetTrigger("Clicked");
                animator.SetBool("Fight", true);
                Hit();
            }
        }

        if (!animator.GetBool("Fight"))
        {
            clicks = 0;
            if (player.curStamina < player.stamina)
                player.curStamina += 1;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fight", false);
            rigidbody2d.AddForce(jumpSpeedVector, ForceMode2D.Impulse);
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
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Foreground"))
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
        if (!facingRight)
            bubbleText.transform.rotation = new Quaternion(0, 180, 0, 0);
        if(facingRight)
            bubbleText.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Hit()
    {
        if (Time.timeScale == 1)
        {
            clicks++;
            if (clicks == 1)
            {
                player.curStamina -= 10;
                audioSource.clip = Resources.Load("Sword1") as AudioClip;
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
                return;
            }
            if (clicks == 2)
            {
                player.curStamina -= 10;
                animator.SetTrigger("secondAttack");
                Vector2 forward = facingRight ? new Vector2(0.5f, 0) : new Vector2(-1, 0);
                rigidbody2d.velocity = forward * speed * Time.deltaTime;
                audioSource.clip = Resources.Load("Sword2") as AudioClip;
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
                return;
            }
            if (clicks == 3)
            {
                player.curStamina -= 10;
                animator.SetTrigger("thirdAttack");
                audioSource.clip = Resources.Load("Sword1") as AudioClip;
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
                clicks = 0;
                return;
            }
        }
    }
}

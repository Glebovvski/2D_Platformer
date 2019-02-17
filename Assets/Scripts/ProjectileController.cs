using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Animator animator;
    private int damage = 30;
    private SpriteRenderer renderer;
    [HideInInspector]
    public PlayerController player;
    private float speed = 3f;
    private Vector3 direction;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        playerPos = player.transform;
        direction = (playerPos.position - transform.position).normalized;
        if (player.transform.position.x > transform.position.x)
            renderer.flipX = false;
        else renderer.flipX = true;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
        Destroy(this.gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().isShielded)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : EnemyController
{
    public override void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        maxHealth = 300;
        curHealth = maxHealth;
        speed = 1.5f;
        damage = 15;
        Experience = 100;
    }

    public override void Update()
    {
        if (animator.GetBool("PlayerSpotted"))
        {
            if (player.transform.position.x > transform.position.x)
                _renderer.flipX = false;
            else _renderer.flipX = true;

            if (Vector2.Distance(transform.position, player.transform.position) > 4)// 0.5f)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        
    }
}

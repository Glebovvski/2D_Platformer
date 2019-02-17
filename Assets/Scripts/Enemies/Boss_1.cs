using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : EnemyController
{
    public Transform scepterSprite;
    private ParticleSystem glowScepter;
    private Light light;

    private bool isGlowingUp = false;
    private bool isGlowingDown = false;
    private bool isAttacking = false;

    [SerializeField]
    private CollectibleItem keyPrefab;

    [SerializeField]
    private ProjectileController projectilePrefab;

    public override void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        glowScepter = GetComponentInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
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
            {
                _renderer.flipX = false;
                scepterSprite.localPosition = new Vector3(0.122f, scepterSprite.localPosition.y, scepterSprite.localPosition.z);
            }
            else
            {
                _renderer.flipX = true;
                scepterSprite.localPosition = new Vector3(-0.128f, scepterSprite.localPosition.y, scepterSprite.localPosition.z);
            }

            if (Vector2.Distance(transform.position, player.transform.position) > 4)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (!isAttacking && !isGlowingDown && light.intensity != 10)
            {
                if (!isGlowingUp)
                {
                    isGlowingUp = true;
                    StartCoroutine(GlowUp());
                }
            }
            if (!isAttacking && !isGlowingUp && light.intensity == 10)
            {
                if (!isGlowingDown)
                {
                    isGlowingDown = true;
                    StartCoroutine(GlowDown());
                }
            }
            if (!isAttacking && light.intensity == 10)
            {
                isAttacking = true;
                StartCoroutine(Attacking());
            }
        }
    }

    IEnumerator GlowUp()
    {
        while (light.intensity < 10)
        {
            light.intensity++;
            yield return new WaitForSeconds(0.2f);
        }
        isGlowingUp = false;
    }

    IEnumerator GlowDown()
    {
        while (light.intensity > 0)
        {
            light.intensity--;
            yield return new WaitForSeconds(0.2f);
        }
        isGlowingDown = false;
    }

    IEnumerator Attacking()
    {
        for (int i = 0; i < 7; i++)
        {
            ProjectileController projectile = Instantiate(projectilePrefab, scepterSprite.transform.position, Quaternion.identity);
            projectile.player = player;
            yield return new WaitForSeconds(1);
        }
        isAttacking = false;
    }

    public override void Damage(float amount)
    {
        base.Damage(amount);
        if(curHealth <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                CollectibleItem item = Instantiate(keyPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

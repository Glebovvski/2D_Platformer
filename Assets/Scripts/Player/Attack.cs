using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canDamage = true;

    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled)
            return;
        IEnemy enemy = collision.GetComponent<IEnemy>();
        if (enemy != null)
        {
            if (canDamage)
            {
                //animator.SetBool("Fight", true);
                enemy.Damage(player.damage);
                canDamage = false;
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canDamage = true;
    }
}

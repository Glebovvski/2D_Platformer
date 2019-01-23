using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyController parent;

    private void Start()
    {
        parent = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            parent.Attack();
        }
    }
}

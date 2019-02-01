using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCombatController : MonoBehaviour
{
    PlayerController player;

    List<EnemyController> enemies;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        enemies = new List<EnemyController>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemies.Add(enemy);
            player.inCombat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
            enemies.Remove(enemy);
        if (enemies.Count == 0)
            player.inCombat = false;
    }
}

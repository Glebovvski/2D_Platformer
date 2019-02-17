using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyController
{
    private void Start()
    {
        base.Start();
        maxHealth = 100;
        speed = 1;
        damage = 5;
        Experience = 15;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics
{
    public float Health = 100;
    public float Strength = 15;
    public float Stamina = 100;
    public float Dexterity = 0.5f;

    public float currentHealth = 100;
    public int currentXP = 1;

    public Dictionary<InventoryType, int> inventoryList = new Dictionary<InventoryType, int>();

    public int currentLevel = 1;
    public int currentSkillPoints = 0;

    public int coins = 0;
}

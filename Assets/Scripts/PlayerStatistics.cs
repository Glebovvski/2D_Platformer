using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStatistics
{
    public float Health = 100;
    public float Strength = 15;
    public float Stamina = 100;
    public float Dexterity = 0.5f;

    public float currentHealth = 100;
    public int currentXP = 0;

    public Dictionary<InventoryType, int> inventoryList = new Dictionary<InventoryType, int>();

    public int currentLevel = 15;
    public int currentSkillPoints = 0;

    public int coins = 0;

    public bool isRestoringActive = false;
    public float restore = 0;

    public ShopItem[] healthPus;

    public ShopItem[] strengthPus;

    public ShopItem[] staminaPus;

    public ShopItem[] dexterityPus;
}

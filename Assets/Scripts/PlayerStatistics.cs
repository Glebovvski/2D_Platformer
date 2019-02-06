using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
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
    
    public float restore = 0;

    public int currentScene = 0;

    [Serializable]
    public class ShopItemBasic
    {
        public bool isBought;
        public bool isBlocked;
    }

    public List<CollectibleItemBasic> collected = new List<CollectibleItemBasic>();

    public ShopItemBasic[] healthPus = new ShopItemBasic[0];

    public ShopItemBasic[] strengthPus = new ShopItemBasic[0];

    public ShopItemBasic[] staminaPus = new ShopItemBasic[0];

    public ShopItemBasic[] dexterityPus = new ShopItemBasic[0];
}


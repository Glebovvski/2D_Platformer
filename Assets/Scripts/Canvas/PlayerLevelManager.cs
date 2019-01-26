﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelManager : Manager
{
    private static PlayerLevelManager _instance;

    public static PlayerLevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("PlayerLevel Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public int Level { get; set; }
    public int XP { get; set; }
    public int RequiredXP { get { return (Level * 25); } }
    public int SkillPoints { get; set; }

    [SerializeField]
    private Image XpImage;
    [SerializeField]
    private Text XpText;
    [SerializeField]
    private Image LevelUpImage;
    [SerializeField]
    private Text LevelUpText;

    // Use this for initialization
    void Start () {
        LevelUpImage.fillAmount = 0;
        Level = 1;
        XP = 0;
        XpImage.fillAmount = XP / RequiredXP;
        XpText.text = XP.ToString() + "/" + RequiredXP.ToString();
        SkillPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GainXP(int amount)
    {
        XP += amount;
        while (XP >= RequiredXP)
        {
            XP -= RequiredXP;
            Level++;
            SkillPoints++;
            LevelUpText.text = SkillPoints + " skill points available";
            StartCoroutine(LevelUp());
        }
        XpImage.fillAmount = (float)XP / (float)RequiredXP;
        XpText.text = XP.ToString() + "/" + RequiredXP.ToString();
    }

    IEnumerator LevelUp()
    {
        while (LevelUpImage.fillAmount != 1)
        {
            LevelUpImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            LevelUpImage.fillAmount += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        while (LevelUpImage.fillAmount != 0)
        {
            LevelUpImage.fillOrigin = (int)Image.OriginVertical.Top;
            LevelUpImage.fillAmount -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        LevelUpText.text = string.Empty;
    }
}
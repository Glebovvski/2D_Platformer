using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {

    public int Level { get; set; }
    public int XP { get; set; }
    public int RequiredXP { get { return (Level * 25); } }
    public int SkillPoints { get; set; }
    // Use this for initialization
    void Start () {
        Level = 1;
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
        }
    }
}

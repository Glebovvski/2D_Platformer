using System.Collections;
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
    private TMPro.TextMeshProUGUI XpText;
    [SerializeField]
    private Image LevelUpImage;
    [SerializeField]
    private TMPro.TextMeshProUGUI LevelUpText;

    // Use this for initialization
    void Start () {
        LevelUpImage.fillAmount = 0;
        UpdateXP();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GainXP(int amount)
    {
        UICanvas.Instance.audioSource.PlayOneShot(UICanvas.Instance.gainXpSound);
        XP += amount;
        while (XP >= RequiredXP)
        {
            XP -= RequiredXP;
            Level++;
            SkillPoints++;
            SkillsManager.Instance.UpdateStats();
            SkillsManager.Instance.UpdateSkillPoints();
            LevelUpText.text = SkillPoints + " skill points available";
            StartCoroutine(LevelUp());
        }
        UpdateXP();
    }

    public void UpdateXP()
    {
        XpImage.fillAmount = (float)XP / (float)RequiredXP;
        XpText.text = XP.ToString() + "/" + RequiredXP.ToString();
    }

    IEnumerator LevelUp()
    {
        UICanvas.Instance.audioSource.PlayOneShot(UICanvas.Instance.levelUpSound);
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

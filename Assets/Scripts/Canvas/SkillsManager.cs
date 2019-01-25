using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour {

    
    private PlayerController player;

    private const float baseHealth = 100;
    private const float baseDext = 0.5f;
    private const float baseStrength = 15;
    private const float baseStamina = 100;

    [SerializeField]
    private Text HealthStats;
    [SerializeField]
    private Text StrengthStats;
    [SerializeField]
    private Text StaminaStats;
    [SerializeField]
    private Text DexStats;

    [SerializeField]
    private Text Level;
    [SerializeField]
    private Text SkillPoints;

    [SerializeField]
    private Text DescriptionText;

    [SerializeField]
    private Button[] MinButtons;
    [SerializeField]
    private Button[] MaxButtons;

    [SerializeField]
    private Button ConfirmBtn;

    private float healthIncreaser = 1.1f;
    private float staminaIncreaser = 1.1f;
    private float strengthIncreaser = 1.1f;
    private float dexterityIncreaser = 1.2f;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
        InteractMinButtons(false);
        InteractMaxButtons(false);
        ConfirmBtn.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.PlayerLevel.SkillPoints > 0)
        {
            InteractMaxButtons(true);
        }

        HealthStats.text = player.player.health.ToString();
        StrengthStats.text = player.player.damage.ToString();
        StaminaStats.text = player.player.stamina.ToString();
        DexStats.text = player.player.dexterity.ToString();
        Level.text = player.PlayerLevel.Level.ToString();
        SkillPoints.text = player.PlayerLevel.SkillPoints.ToString();
	}

    private void InteractMinButtons(bool interact)
    {
        foreach (var btn in MinButtons)
        {
            btn.interactable = interact;
        }
    }

    private void InteractMinButtons(bool interact, int index)
    {
        MinButtons[index].interactable = interact;
    }

    private void InteractMaxButtons(bool interact)
    {
        foreach (var btn in MaxButtons)
        {
            btn.interactable = interact;
        }
    }

    bool InteractableMinBtn(int index)
    {
        return MinButtons[index].interactable;
    }

    bool InteractableMaxBtns()
    {
        foreach (var btn in MaxButtons)
        {
            if (!btn.interactable)
                return false;
        }
        return true;
    }

    void CheckSPLeft(int index)
    {
        if (player.PlayerLevel.SkillPoints == 0)
        {
            InteractMaxButtons(false);
            InteractMinButtons(true, index);
        }
        else
        {
            InteractMaxButtons(true);
            InteractMinButtons(true, index);
        }
    }

    public void IncreaseHealth()
    {
        if (player.PlayerLevel.SkillPoints > 0)
        {
            player.player.health *= healthIncreaser;
            player.PlayerLevel.SkillPoints--;
            CheckSPLeft(0); //check which stat was increased
            ConfirmBtn.interactable = true;
        }
    }

    public void DecreaseHealth()
    {
        if (InteractableMinBtn(0))
        {
            player.player.health /= healthIncreaser;
            player.PlayerLevel.SkillPoints++;
            if (player.player.health != baseHealth)
                CheckSPLeft(0);
            else InteractMinButtons(false, 0);
            ConfirmBtn.interactable = true;
        }
    }

    public void IncreaseStamina()
    {
        if (player.PlayerLevel.SkillPoints > 0)
        {
            player.player.stamina *= staminaIncreaser;
            player.PlayerLevel.SkillPoints--;
            CheckSPLeft(2);
            ConfirmBtn.interactable = true;
        }
    }

    public void DecreaseStamina()
    {
        if (InteractableMinBtn(2))
        {
            player.player.stamina /= staminaIncreaser;
            player.PlayerLevel.SkillPoints++;
            if (player.player.stamina != baseStamina)
                CheckSPLeft(2);
            else InteractMinButtons(false, 2);
            ConfirmBtn.interactable = true;
        }
    }

    public void IncreaseStrength()
    {
        if (player.PlayerLevel.SkillPoints > 0)
        {
            player.player.damage *= strengthIncreaser;
            player.PlayerLevel.SkillPoints--;
            CheckSPLeft(1);
            ConfirmBtn.interactable = true;
        }
    }

    public void DecreaseStrength()
    {
        if (InteractableMinBtn(1))
        {
            player.player.damage /= strengthIncreaser;
            player.PlayerLevel.SkillPoints++;
            if (player.player.damage != baseStrength)
                CheckSPLeft(1);
            else InteractMinButtons(false, 1);
            ConfirmBtn.interactable = true;
        }
    }

    public void IncreaseDexterity()
    {
        if (player.PlayerLevel.SkillPoints > 0)
        {
            player.player.dexterity *= dexterityIncreaser;
            player.PlayerLevel.SkillPoints--;
            CheckSPLeft(3);
            ConfirmBtn.interactable = true;
        }
    }

    public void DecreaseDexterity()
    {
        if (InteractableMinBtn(3))
        {
            player.player.dexterity /= dexterityIncreaser;
            player.PlayerLevel.SkillPoints++;
            if (player.player.dexterity != baseDext)
                CheckSPLeft(3);
            else InteractMinButtons(false, 3);
            ConfirmBtn.interactable = true;
        }
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("SkillPoints", player.PlayerLevel.SkillPoints);
        PlayerPrefs.SetFloat("Health", player.player.health);
        PlayerPrefs.SetFloat("Stamina", player.player.stamina);
        PlayerPrefs.SetFloat("Strength", player.player.damage);
        PlayerPrefs.SetFloat("Dexterity", player.player.dexterity);

        ConfirmBtn.interactable = false;
        if (player.PlayerLevel.SkillPoints > 0)
        {
            InteractMaxButtons(true);
        }
        else
            InteractMaxButtons(false);

        InteractMinButtons(false);
    }

    public void DescriptionShow(GameObject stat)
    {
        switch (stat.tag)
        {
            case ("Health"):
                DescriptionText.text = "Health determines amount of Health Points your character has";
                break;
            case ("Stamina"):
                DescriptionText.text = "Stamina determines amount of stamina points your character has";
                break;
            case ("Strength"):
                DescriptionText.text = "Strength determines amount of strength points. Increasing strength will allow you to kill enemies faster";
                break;
            case ("Dexterity"):
                DescriptionText.text = "Dexterity is a measure of how agile your character is. Dexterity controls accuracy, as well as evading an opponent's attack";
                break;
            default:
                break;
        }
    }

    public void DescriptionHide()
    {
        DescriptionText.text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : Manager
{
    private static PlayerStatsManager _instance;

    public static PlayerStatsManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("PlayerStats Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    

    public Slider healthSlider;
    public Slider staminaSlider;

    private void Start()
    {
        healthSlider.maxValue = UICanvas.Instance.player.Health;
        healthSlider.value = UICanvas.Instance.player.curHealth;
        staminaSlider.maxValue = UICanvas.Instance.player.stamina;
        staminaSlider.value = UICanvas.Instance.player.curStamina;
    }

    private void Update()
    {
        staminaSlider.value = UICanvas.Instance.player.curStamina;
    }

    public void UpdateHealth()
    {
        healthSlider.value = UICanvas.Instance.player.curHealth;
    }
}

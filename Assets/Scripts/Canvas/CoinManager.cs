using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : Manager
{
    private static CoinManager _instance;

    public static CoinManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Coin Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Text coinAmountText;

    public void UpdateCoinsAmount(int value)
    {
        coinAmountText.text = value.ToString();
    }
}

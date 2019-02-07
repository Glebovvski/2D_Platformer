using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBtnManager : Manager
{
    private static NavigationBtnManager _instance;

    public static NavigationBtnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("NavBtn Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}

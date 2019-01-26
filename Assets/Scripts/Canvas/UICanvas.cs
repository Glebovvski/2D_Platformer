using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : Manager {

    private static UICanvas _instance;

    public static UICanvas Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("UICanvas instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [HideInInspector]
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour {

    UICanvas Instance;

	// Use this for initialization
	void Start () {
        if (Instance == null)
            Instance = new UICanvas();
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

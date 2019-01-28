using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagament : Manager
{
    private static SceneManagament _instance;

    public static SceneManagament Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Scene Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    //private  HUDcanvas;

    private void Start()
    {
        //HUDcanvas = UICanvas.Instance.GetComponent<Canvas>();//GameObject.Find("HUDCanvas");
        SceneManager.activeSceneChanged += SceneChanged;
    }

    private void SceneChanged(Scene prevScene, Scene curScene)
    {
        if (SceneManager.GetActiveScene().name == "LevelPick") //change comparison to list of names later
        {
            UICanvas.Instance.SetActive(false);
        }
    }

    public void LoadLevel(int level)
    {
        UICanvas.Instance.player.SavePlayer();
        SceneManager.LoadScene(level);
    }
}

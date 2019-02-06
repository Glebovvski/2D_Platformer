using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        SceneManager.sceneLoaded += OnSceneLoaded;

        GlobalControl.Instance.CheckCollectibles();
        if (GlobalControl.Instance.collected.Count > 0)
        {
            if (GlobalControl.Instance.collectibles != null && GlobalControl.Instance.collectibles.Count > 0)
            {
                foreach (var item in GlobalControl.Instance.collectibles)
                {
                    if (GlobalControl.Instance.collected.Where(x => x.name == item.name 
                                                                 && x.item == item.item 
                                                                 && x.sceneIndex == SceneManager.GetActiveScene().buildIndex)
                                                                 .FirstOrDefault() != null)
                    {
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    private void SceneChanged(Scene prevScene, Scene curScene)
    {
        if (SceneManager.GetActiveScene().name == "LevelPick") //change comparison to list of names later
        {
            UICanvas.Instance.player.HUDIsOpen = true;
            UICanvas.Instance.SetActive(false);
        }
    }

    public void LoadLevel(int level)
    {
        GlobalControl.Instance.SceneLevel = SceneManager.GetActiveScene().buildIndex;
        UICanvas.Instance.player.SavePlayer();
        SceneManager.LoadScene(level);
        GlobalControl.Instance.savedPlayerData.currentScene = level;
        GlobalControl.Instance.savedPlayerData.collected = GlobalControl.Instance.collected;
        GlobalControl.Instance.Save();
    }

    private void OnLevelWasLoaded(int level)
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
            GlobalControl.Instance.SceneLevel = scene.buildIndex;
    }
}

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
        if (GlobalControl.Instance.collectibles != null && GlobalControl.Instance.collectibles.Count > 0)
        {
            foreach (var item in GlobalControl.Instance.collectibles)
            {
                if (GlobalControl.Instance.collected.Contains(item))
                    item.gameObject.SetActive(false);
                //if (GlobalControl.Instance.collected.Where(x=>x.name == item.name).FirstOrDefault())
                //{
                //    Destroy(item.gameObject);
                //    Debug.Log("Destroyed");
                //}
            }
        }
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
        //GlobalControl.Instance.Save();
    }

    private void OnLevelWasLoaded(int level)
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagament : MonoBehaviour
{
    private GameObject HUDcanvas;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
    }

    private void SceneChanged(Scene prevScene, Scene curScene)
    {
        HUDcanvas = GameObject.Find("HUDCanvas");
        if (SceneManager.GetActiveScene().name == "LevelPick") //change comparison to list of names later
        {
            if (HUDcanvas != null)
                HUDcanvas.gameObject.SetActive(false);
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagament : MonoBehaviour
{
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        SceneManager.activeSceneChanged += SceneChanged;
        
    }

    private void SceneChanged(Scene prevScene, Scene curScene)
    {
        if (SceneManager.GetActiveScene().name == "LevelPick") //change comparison to list of names later
        {
            if (canvas != null)
                canvas.gameObject.SetActive(false);
        }
    }
}

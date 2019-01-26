using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheNextLevelManager : Manager
{
    private static ToTheNextLevelManager _instance;

    public static ToTheNextLevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Dialogue Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SetActive(false);
    }

    //[SerializeField]
    //private GameObject panel;

    public void OpenPanel()
    {
        //InventoryManager.Instance.player.HUDIsOpen = true;
        UICanvas.Instance.player.HUDIsOpen = true;
        SetActive(true);
    }

    public void ClosePanel()
    {
        SetActive(false);
    }

    public void LeaveBtn()
    {
        //LoadScene stuff
        SceneManager.LoadScene("LevelPick");
        SetActive(false);
    }
}

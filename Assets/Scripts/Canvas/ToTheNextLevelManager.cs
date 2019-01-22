using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheNextLevelManager : MonoBehaviour
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
        ClosePanel();
    }

    [SerializeField]
    private GameObject panel;

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void LeaveBtn()
    {
        //LoadScene stuff
    }
}

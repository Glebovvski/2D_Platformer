using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NavigationManager : Manager
{
    private static NavigationManager _instance;

    public static NavigationManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Navigation Manager instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private List<GameObject> panels;
    [SerializeField]
    private Text currentPanelLabel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (panels[0].activeInHierarchy)
            currentPanelLabel.text = "Inventory [I]";
        if (panels[1].activeInHierarchy)
            currentPanelLabel.text = "Skills [K]";

        if(panels.Any(x=>x.activeInHierarchy))
        {
            if (Input.GetKeyDown(KeyCode.E))
                NextPanel();
            if (Input.GetKeyDown(KeyCode.Q))
                PrevPanel();
        }
    }

    public void NextPanel()
    {
        if (panels[0].activeInHierarchy)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
        else if (panels[1].activeInHierarchy)
        {
            panels[1].SetActive(false);
            panels[0].SetActive(true);
        }
    }

    public void PrevPanel()
    {
        if (panels[0].activeInHierarchy)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
        else if (panels[1].activeInHierarchy)
        {
            panels[1].SetActive(false);
            panels[0].SetActive(true);
        }
    }
}

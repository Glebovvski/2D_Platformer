using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorController : MonoBehaviour
{
    private static BackDoorController _instance;

    public static BackDoorController Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("BackDoor Controller instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    public bool isDoorOpened;

    List<int> keysAmount;

    // Start is called before the first frame update
    void Start()
    {
        isDoorOpened = true;
    }

    public void OpenDoor()
    {
        isDoorOpened = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDoorOpened)
        {
            if (other.tag == "Player")
                ToTheNextLevelManager.Instance.OpenPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDoorOpened)
        {
            if (other.tag == "Player")
                ToTheNextLevelManager.Instance.ClosePanel();
        }
    }
}

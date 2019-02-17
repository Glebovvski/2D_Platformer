using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
using Assets.Scripts.Enemies;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    private static DoorController _instance;

    public static DoorController Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Door Controller instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private CinemachineVirtualCamera Camera;

    private int counts = 0;

    public bool isDoorOpened;

    List<int> keysAmount;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalControl.Instance.savedPlayerData.collected.Where(x => x.sceneIndex == SceneManager.GetActiveScene().buildIndex).ToList().Count > 0)
        {
            isDoorOpened = true;
            this.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        }
        else
            isDoorOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenDoor()
    {
        keysAmount = UICanvas.Instance.player.inventoryList.Where(x => x.Key == InventoryType.Key).Select(x => x.Value).ToList();
        isDoorOpened = true;
        Camera.Priority = 11;
        StartCoroutine(Wait());
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

    private IEnumerator Wait()
    {
        FreezeAll();
        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
        //StopCoroutine(Wait());
        StartCoroutine(BackToPlayer());
    }

    private IEnumerator BackToPlayer()
    {
        FreezeAll();
        yield return new WaitForSeconds(2);
        Camera.Priority = 9;

        for (int i = 0; i < keysAmount[0]; i++)
        {
            InventoryManager.Instance.RemoveFromInventory(InventoryType.Key);
        }
    }

    private void FreezeAll()
    {
        var componentsWithRigidBody = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        componentsWithRigidBody.ForEach(x => x.GetComponent<IEnemy>().Stop());
    }
}

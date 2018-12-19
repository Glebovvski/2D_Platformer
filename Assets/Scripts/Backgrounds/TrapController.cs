using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] traps;

    // Use this for initialization
    void Start()
    {
        int n = this.gameObject.transform.childCount;
        traps = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            traps[i] = this.transform.GetChild(i).gameObject;
            traps[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && traps!=null)
        {
            for (int i = 0; i < traps.Length; i++)
            {
                if (traps[i] != null)
                {
                    traps[i].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < traps.Length; i++)
            {
                Destroy(traps[i]);
            }
        }
    }
}

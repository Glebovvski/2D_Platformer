using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    [SerializeField]
    private InventoryType item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(item);
            GlobalControl.Instance.collected.Add(this);
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    [SerializeField]
    private InventoryType item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Inventory.GetComponent<InventoryManager>().AddItem(item);
            Destroy(this.gameObject);
        }
    }
}

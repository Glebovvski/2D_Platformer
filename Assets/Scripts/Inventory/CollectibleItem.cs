using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleItem : MonoBehaviour, ICloneable
{

    [SerializeField]
    public InventoryType item;

    public CollectibleItem()
    {

    }

    public CollectibleItem(InventoryType type)
    {
        item = type;
    }

    public object Clone()
    {
        return new CollectibleItemBasic() { name = this.gameObject.name, item = this.item, sceneIndex = SceneManager.GetActiveScene().buildIndex };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(item);
            //CollectibleItem clone = 
            GlobalControl.Instance.collected.Add((CollectibleItemBasic)this.Clone());
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}

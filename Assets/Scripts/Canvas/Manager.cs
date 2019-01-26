using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public virtual void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager.Instance.SetActive(!InventoryManager.Instance.gameObject.activeInHierarchy);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SkillsManager.Instance.SetActive(!SkillsManager.Instance.gameObject.activeInHierarchy);
        }
    }
}

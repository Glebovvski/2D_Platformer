using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public virtual void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}

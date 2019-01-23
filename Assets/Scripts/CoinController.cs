using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [HideInInspector]
    public int value = 0;

    //private void Start()
    //{
    //    value = Random.Range(20, 25);
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddCoin(value);
                Destroy(this.gameObject);
            }
        }
    }
}

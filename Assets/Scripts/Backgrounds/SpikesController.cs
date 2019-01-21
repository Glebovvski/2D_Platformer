using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemies;

public class SpikesController : MonoBehaviour {

    public int damage;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().isShielded)
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IEnemy>().Damage(damage);
        }
    }
}

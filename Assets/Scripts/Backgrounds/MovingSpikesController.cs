using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikesController : MonoBehaviour {

    public int damage;
    [SerializeField]
    private float speed = 5f;

    // Use this for initialization
    void Start() {
        //transform.Rotate(new Vector3(0, 0, 90));
    }

    // Update is called once per frame
    void Update() {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnEnable()
    {
        Invoke("Destroy", 1f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Destroy(float time)
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().isShielded)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                Destroy(0);
            }
        }
    }
}

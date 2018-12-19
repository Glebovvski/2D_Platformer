using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingObject : MonoBehaviour {

    public bool horizontal;

    private float moveTime = 0.5f;
   
    private float direction = 1;
    
    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
    }


    private void Update()
    {
        if (horizontal)
            transform.position = new Vector3(transform.position.x + moveTime * Time.smoothDeltaTime * direction, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x , transform.position.y + moveTime * Time.smoothDeltaTime * direction);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.parent = transform;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Foreground"))
            direction *= -1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.parent = null;
    }

}

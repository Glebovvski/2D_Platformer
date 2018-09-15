using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    private float moveTime = 0.5f;

    private float direction = 1;
    
    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
    }


    private void Update()
    {
        transform.position = new Vector3(transform.position.x + moveTime *Time.smoothDeltaTime * direction, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Foreground"))
            direction *= -1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public bool clockwise = true;
    private float moveTime = 15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!clockwise)
            transform.Rotate(0, 0, moveTime * Time.deltaTime);
        else
            transform.Rotate(0, 0, moveTime * Time.deltaTime * -1);
    }
}

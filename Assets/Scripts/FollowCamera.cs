using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera camera;

    public bool move;
    
    private Transform LeftBarier;
    private Transform RightBarier;
    private Transform UpBarier;

    private void Start()
    {
        move = true;
        LeftBarier = GameObject.Find("LeftBarierActual").GetComponent<Transform>();// +target.position;
        RightBarier = GameObject.Find("RightBarier").GetComponent<Transform>();// -target.position;
        UpBarier = GameObject.Find("UpBarier").GetComponent<Transform>();// +target.position;
        camera = GetComponent<Camera>();
        //camera.transform.position = new Vector3(target.position.x,target.position.y,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.25f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            //if (destination.x > LeftBarier.position.x-target.position.x && destination.x < RightBarier.position.x-target.position.x || destination.y < UpBarier.position.y - target.position.y)
            //if (camera.transform.position.x > (LeftBarier.position.x+2.04) || camera.transform.position.x < (RightBarier.position.x - 2.04))
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}

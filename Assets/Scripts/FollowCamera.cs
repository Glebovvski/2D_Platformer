using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera camera;

    
    private Vector3 LeftBarier;
    private Vector3 RightBarier;
    private Vector3 UpBarier;

    private void Start()
    {
        LeftBarier = GameObject.Find("LeftBarier").GetComponent<Transform>().position +target.position;
        RightBarier = GameObject.Find("RightBarier").GetComponent<Transform>().position -target.position;
        UpBarier = GameObject.Find("UpBarier").GetComponent<Transform>().position+target.position;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.25f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = new Vector3(Mathf.Clamp((transform.position.x + delta.x), LeftBarier.x, RightBarier.x), Mathf.Clamp((transform.position.y + delta.y), -10, UpBarier.y), transform.position.z);
            //if (destination.x > LeftBarier.position.x-target.position.x && destination.x < RightBarier.position.x-target.position.x || destination.y < UpBarier.position.y - target.position.y)
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}

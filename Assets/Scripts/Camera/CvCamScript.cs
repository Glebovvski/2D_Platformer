using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CvCamScript : MonoBehaviour {

    [SerializeField]
    Cinemachine.CinemachineVirtualCamera camera;
    
    [SerializeField]
    private Transform player;

    private float speed = 3.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.S))
        {
            camera.Priority = 11;
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        else
        {
            var dist = camera.transform.position;
            camera.Priority = 9;
        }
    }
}

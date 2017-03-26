using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanderingMovement : MonoBehaviour {

    private Rigidbody body;
    private float minX, maxX;
    public float speed = 40;
    public float tiltAngle = 10;  // Max tilt
    public float zLimit;
    private float currentTilt = 0;
    private bool rightDirection = true;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

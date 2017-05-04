using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanderingMovement : MonoBehaviour {

    private Rigidbody body;
    private float minX, maxX;
    public float maxSpeed = 40;
    public float secondsPerCicle = 2;
    public float stopSeconds = 1;
    private float currSpeed = 40;
    private float step;
    private bool speedingUp = false;
    //public float tiltAngle = 10;  // Max tilt
    //public float zLimit;
    //private float currentTilt = 0;
    //private bool rightDirection = true;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        //step = maxSpeed / 
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

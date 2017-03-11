﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndulantMovement : MonoBehaviour {

    private Rigidbody body;
    private float minX, maxX;
    public float speed = 40;
    public float tiltAngle = 90;  // Max tilt
    private float currentTilt = 0;
    private bool rightDirection = true;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

    void Start () {
        UpdateAreaLimits();
        body.velocity = new Vector3(speed, 0, 0);
        //body.rotation = Quaternion.Euler(0, 0, -tiltAngle);
    }
	
	void Update () {

        // Update body direction when it reach the limits
		if(body.transform.position.x > maxX) {
            body.velocity = new Vector3(-speed, 0, 0);
            rightDirection = false;
            //body.rotation = Quaternion.Euler(0, 0, tiltAngle);
        } else if (body.transform.position.x < minX) {
            body.velocity = new Vector3(speed, 0, 0);
            rightDirection = true;
            //body.rotation = Quaternion.Euler(0, 0, -tiltAngle);
        }

        // Smooth tilt
        if((rightDirection && currentTilt > -tiltAngle) ||
            (!rightDirection && currentTilt < tiltAngle)) {
            currentTilt += (rightDirection) ? -tiltAngle / 10 : tiltAngle / 10;
            body.rotation = Quaternion.Euler(0, 0, currentTilt);
        }
        
    }

    void UpdateAreaLimits() {
        Vector2 viewSize = Utils.GetViewDimensions();
        minX = -viewSize.x / 2 + 12;
        maxX = viewSize.x / 2 - 12;
    }
}
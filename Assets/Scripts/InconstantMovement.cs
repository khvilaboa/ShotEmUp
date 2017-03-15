using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InconstantMovement : MonoBehaviour {

    public Vector3 maxSpeed;
    public Vector3 minSpeed;
    public float intervalSeconds = 0.5f;
    public float stoppedSeconds = 0.2f;
    public int stepsPerCicle = 10;

    private Rigidbody body;
    private float currentStep;
    private float stepTime;
    private float nextStep;
    
    private bool increasingSpeed = false;
    private Vector3 step;

    void Awake () {
        currentStep = 0;
        stepTime = intervalSeconds / stepsPerCicle;
        step = -(maxSpeed - minSpeed) / (stepsPerCicle / 2f);
        nextStep = Time.time + stepTime;
        increasingSpeed = false;
        body = GetComponent<Rigidbody>();
    }

    void Start() {
        body.velocity = maxSpeed;
    }

	void Update () {
		if(Time.time > nextStep) {
            nextStep = Time.time + stepTime;
            body.velocity += step;
            Debug.Log(body.velocity + "," + currentStep+ "," + stepTime);

            currentStep += 1;
            if (currentStep % (stepsPerCicle / 2) == 0) {
                step *= -1;
                increasingSpeed = !increasingSpeed;
                if(increasingSpeed) {
                    nextStep = Time.time - stepTime + stoppedSeconds;
                }
            }
        }
	}
}

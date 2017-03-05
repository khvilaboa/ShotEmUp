using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovement : MonoBehaviour {

    private Rigidbody body;
    public float speed;

	void Start () {
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(0, 0, speed);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : MonoBehaviour {

    private Rigidbody body;
    public Vector3 direction;
    public float yLimit;

	void Start () {
        body = GetComponent<Rigidbody>();
        body.velocity = direction;
	}
}

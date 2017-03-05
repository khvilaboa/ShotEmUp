using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private Rigidbody body;
    public float speedLimit;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

	void Start () {
        body.angularVelocity = new Vector3(Random.Range(-speedLimit, speedLimit),
                                           Random.Range(-speedLimit, speedLimit),
                                           Random.Range(-speedLimit, speedLimit));
	}

    void OnTriggerEnter(Collider coll) {
        if(coll.tag != "boundary") {
            Destroy(coll.gameObject);  // Shot
            Destroy(gameObject);  // Asteroid
        }
    }
}

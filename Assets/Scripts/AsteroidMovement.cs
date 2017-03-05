using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private Rigidbody body;
    public float speedLimit;
    public GameObject explosion;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

	void Start () {
        // Random rotation
        body.angularVelocity = new Vector3(Random.Range(-speedLimit, speedLimit),
                                           Random.Range(-speedLimit, speedLimit),
                                           Random.Range(-speedLimit, speedLimit));

        // Random size 
        float scale = Random.Range(4, 8);
        transform.localScale = new Vector3(scale, scale, scale);
	}

    void OnTriggerEnter(Collider coll) {
        if(coll.tag != "boundary") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(coll.gameObject);  // Shot
            Destroy(gameObject);  // Asteroid
        }
    }
}

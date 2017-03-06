using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    private Rigidbody body;
    public float angularSpeedLimit;
    public GameObject explosion;
    public float asteroidMinSpeed;
    public float asteroidMaxSpeed;
    public float asteroidMinSize;
    public float asteroidMaxSize;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

	void Start () {
        // Random rotation
        body.angularVelocity = new Vector3(Random.Range(-angularSpeedLimit, angularSpeedLimit),
                                           Random.Range(-angularSpeedLimit, angularSpeedLimit),
                                           Random.Range(-angularSpeedLimit, angularSpeedLimit));

        // Random size 
        float scale = Random.Range(4, 8);
        transform.localScale = new Vector3(scale, scale, scale);

        // Random velocity
        body.velocity = new Vector3(0, 0, -Random.Range(asteroidMinSpeed, asteroidMaxSpeed));
	}

    void OnTriggerEnter(Collider coll) {
        if(coll.tag != "Boundary") {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(coll.gameObject);  // Shot
            Destroy(gameObject);  // Asteroid
        }
    }
}

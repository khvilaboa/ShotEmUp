using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovement : MonoBehaviour {

    private Rigidbody body;
    public GameObject target;
    public float maxSpeed;
    public float tiltAngle;
    public bool zLimited = true;
    public float zLimit;
    

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if(zLimited) body.velocity = new Vector3(body.velocity.x, body.velocity.y, -maxSpeed);
    }

    void FixedUpdate () {
        if (zLimited && body.position.z <= zLimit) body.velocity = new Vector3(body.velocity.x, body.velocity.y, 0);

        if(target != null) {
            float diff = target.transform.position.x - transform.position.x;
            body.velocity = new Vector3(Mathf.Clamp(diff, -maxSpeed, maxSpeed), body.velocity.y, body.velocity.z);

            body.rotation = Quaternion.Euler(0, 0, -tiltAngle * (diff / maxSpeed));
        }

        
    }

}

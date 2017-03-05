using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    

    [Header("Movement")]
    public float speed;
    public float tiltAngle;
    public float minX, maxX, minZ, maxZ;
    private Rigidbody body;

    void Start () {
        body = GetComponent<Rigidbody>();
	}

    void FixedUpdate () {
        // Get movements from inputs
        float horizontalMov = Input.GetAxis("Horizontal");
        float verticalMov = Input.GetAxis("Vertical");

        // Set the new volocity
        Vector3 movementVector = new Vector3(horizontalMov, 0, verticalMov);
        body.velocity = movementVector * speed;

        // Check that the player is inside the boundaries
        float newX = Mathf.Clamp(body.position.x, minX, maxX);
        float newZ = Mathf.Clamp(body.position.z, minZ, maxZ);
        body.position = new Vector3(newX, 0, newZ);

        // Tilt the player when moving
        float currentTiltAngle = -(body.velocity.x / speed) * tiltAngle;
        body.rotation = Quaternion.Euler(0, 0, currentTiltAngle);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement")]
    public float speed;
    public float tiltAngle;
    public float minX, maxX, minZ, maxZ;
    private Rigidbody body;

    [Header("Shooting")]
    public ShotController shotController;
    public float fireRate;  // In seconds
    private float nextFire;
    

    void Awake () {
        body = GetComponent<Rigidbody>();
        nextFire = 0;
	}

    void Start() {
        UpdateAreaLimits();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            shotController.Fire();
            nextFire = Time.time + fireRate;
        }
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

    // Limits the area in which the player can move
    void UpdateAreaLimits() {
        Vector2 viewSize = Utils.GetViewDimensions();
        minX = -viewSize.x / 2 + 8;
        maxX = viewSize.x / 2 - 8;
        minZ = 0;
        maxZ = 3f * viewSize.y / 4;
    }

}

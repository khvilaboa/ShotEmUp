using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum InputMode
    {
        OnlyKeyboard,
        OnlyGesture,
        KeyboardAndGesture
    };

    [Header("Gesture Controller")]
    public GestureInputController gestureController;

    public InputMode inputMode;

    [Header("Movement")]
    public float speed;
    public float tiltAngle;
    public float minX, maxX, minZ, maxZ;
    private Rigidbody body;

    [Header("Shooting")]
    public ShotController shotController;
    public float fireRate;  // In seconds
    private float nextFire;


    void Awake()
    {
        body = GetComponent<Rigidbody>();
        nextFire = 0;
    }

    void Start()
    {
        UpdateAreaLimits();
        if (GameOptions.inputModeSelected == GameOptions.InputMode.OnlyGesture)
        {
            inputMode = InputMode.OnlyGesture;
        }
        else
        {
            inputMode = InputMode.OnlyKeyboard;
        }
    }

    void Update()
    {
        
        if (Time.time >= nextFire && ((inputMode == InputMode.OnlyKeyboard && Input.GetButton("Fire1")) || (inputMode != InputMode.OnlyKeyboard && gestureController.GetButton("Fire1"))))
        {
            shotController.Fire();
            nextFire = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        float horizontalMov = 0;
        float verticalMov = 0;
        // Get movements from inputs

        if (inputMode == InputMode.OnlyKeyboard)
        {
            horizontalMov = Input.GetAxis("Horizontal");
            verticalMov = Input.GetAxis("Vertical");
        }
        else if (inputMode == InputMode.OnlyGesture)
        {
            horizontalMov = gestureController.GetAxis("Horizontal");
            verticalMov = gestureController.GetAxis("Vertical");
        }
        else if (inputMode == InputMode.KeyboardAndGesture)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                horizontalMov = Input.GetAxis("Horizontal");
                verticalMov = Input.GetAxis("Vertical");
            }
            else
            {
                horizontalMov = gestureController.GetAxis("Horizontal");
                verticalMov = gestureController.GetAxis("Vertical");
            }
        }


        //Debug.Log(gestureController.GetHandPosition());
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
    void UpdateAreaLimits()
    {
        Vector2 viewSize = Utils.GetViewDimensions();
        minX = -viewSize.x / 2 + 8;
        maxX = viewSize.x / 2 - 8;
        minZ = 0;
        maxZ = 3f * viewSize.y / 4;
    }

}

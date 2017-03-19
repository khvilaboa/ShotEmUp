using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitantMovement : MonoBehaviour {

    public Transform center;
    public Vector3 axis = Vector3.up;
    public float radius = 15;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 40;
    public DestroyController destroyController;
    private Vector3 lastCenter;

    void Start()
    {
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        lastCenter = center.position;
    }

    void FixedUpdate()
    {
        if(center != null)
        {
            transform.position += (center.position - lastCenter);
            transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.identity;
            lastCenter = center.position;
        }
        else if(destroyController != null)
        {
            destroyController.Destroy();
        }
        
    }
}

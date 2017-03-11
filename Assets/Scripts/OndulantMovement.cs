using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndulantMovement : MonoBehaviour {

    private Rigidbody body;
    public float minX, maxX;
    public float speed = 40;

    void Awake() {
        body = GetComponent<Rigidbody>();
    }

    void Start () {
        UpdateAreaLimits();
        body.velocity = new Vector3(speed, 0, 0);
    }
	
	void Update () {
		if(body.transform.position.x > maxX) {
            body.velocity = new Vector3(-speed, 0, 0);
        } else if (body.transform.position.x < minX) {
            body.velocity = new Vector3(speed, 0, 0);
        }

    }

    void UpdateAreaLimits() {
        Vector2 viewSize = Utils.GetViewDimensions();
        minX = -viewSize.x / 2 + 16;
        maxX = viewSize.x / 2 - 16;
    }
}

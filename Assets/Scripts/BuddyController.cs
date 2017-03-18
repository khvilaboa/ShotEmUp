using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyController : MonoBehaviour {

    public ShotController shotController;
    public float fireRate = 0.75f;
    private float nextFire;

	void Start () {
        nextFire = Time.time + fireRate;
	}

    void Update()
    {
        if (shotController != null && Time.time >= nextFire)
        {
            shotController.Fire();
            nextFire = Time.time + fireRate;
        }
    }
}

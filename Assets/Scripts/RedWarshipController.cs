using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWarshipController : MonoBehaviour {

    public ShotController shotController;
    public GameObject target;
    public float fireRate = 0.75f;
    private float nextFire;

	void Start () {
        nextFire = Time.time + fireRate;
	}

    void Update()
    {
        if (shotController != null && Time.time >= nextFire)
        {
            shotController.FireTarget(target);
            nextFire = Time.time + fireRate;
        }
    }
}

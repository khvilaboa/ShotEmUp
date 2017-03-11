using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public ShotController shotController;
    public float fireRate;
    private float nextFire;

    void Start () {
        shotController = GetComponent<ShotController>();
        nextFire = 0;
    }

    void Update()
    {
        if (Time.time >= nextFire)
        {
            bool burstShot = Random.Range(0, 10) < 9;  // 90%
            if (burstShot)
            {
                shotController.Burst(fireRate);
                nextFire = Time.time + 2 * fireRate;  // Burst duration + fireRate
            }
            else {
                shotController.Fire();
                nextFire = Time.time + fireRate;
            }
        }
    }
}

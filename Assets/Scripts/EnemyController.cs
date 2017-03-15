using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public ShotController shotController;
    public float fireRate;
    public float initialWait = 2;
    private float nextFire;

    void Start () {
        shotController = GetComponent<ShotController>();
        nextFire = Time.time + initialWait;
    }

    void Update()
    {
        if (Time.time >= nextFire)
        {
            bool burstShot = Random.Range(0, 10) < 8;  // 80%
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

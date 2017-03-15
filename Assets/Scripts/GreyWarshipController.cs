using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyWarshipController : MonoBehaviour {

    public ShotController shotController;
    public float fireRate = 0.2f;
    public float initialWait = 2;
    public float waitBetweenBursts = 1;
    public int shotsPerBurst = 5;
    private float nextFire;
    private int currBurst;

    void Awake()
    {
        shotController = GetComponent<ShotController>();
        nextFire = Time.time + initialWait;
        currBurst = 0;
    }

    void Update()
    {
        if (Time.time >= nextFire)
        {
            switch(Random.Range(0,4))
            {
                case 0: shotController.Fire(0); shotController.Fire(6); break;
                case 1: shotController.Fire(1); shotController.Fire(5); break;
                case 2: shotController.Fire(2); shotController.Fire(4); break;
                case 3: shotController.Fire(3); break;
            }

            currBurst += 1;
            
            if(currBurst >= shotsPerBurst) {
                currBurst = 0;
                nextFire = Time.time + waitBetweenBursts;
            } else {
                nextFire = Time.time + fireRate;
            }
        }
    }
}
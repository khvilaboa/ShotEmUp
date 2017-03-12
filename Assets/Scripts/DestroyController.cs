using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour {

    private const int SCORE_ASTEROID = 10;

    private GameController gameController;
    public GameObject explosion;
    public int health = 100;
    public Transform healthMarker;
    private float healthMarkerLength;
    private float healthMarkerInitialPos;
    private int fullHealth;

    void Awake() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        fullHealth = health;
    }

    void Start() {
        if(healthMarker != null) {
            healthMarkerLength = healthMarker.GetChild(0).localScale.y;
            healthMarkerInitialPos = healthMarker.GetChild(0).position.z;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Boundary") return;
        if (tag == "Enemy" && (coll.tag == "Enemy" || coll.tag == "EnemyShot")) return;
        if (tag == "Player" && coll.tag == "PlayerShot") return;        

        if (coll.tag == "PlayerShot" || coll.tag == "EnemyShot") Destroy(coll.gameObject);
        Debug.Log(coll.name);

        health -= 10;
        if (health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            
            if (tag == "Player") gameController.GameOver();
        } else if (healthMarker != null) {
            float step = healthMarkerLength * (10f / fullHealth);
            healthMarker.GetChild(0).localScale -= new Vector3(0, step, 0);
            healthMarker.GetChild(0).position -= new Vector3(step, 0, 0);
            healthMarker.GetChild(1).localScale += new Vector3(0, step, 0);
            healthMarker.GetChild(1).position -= new Vector3(step, 0, 0);
            //healthMarker.GetChild(0).localScale = new Vector3(scale0.x, scale0.y, healthMarkerLength * ((float)health / fullHealth));
            //Vector3 scale1 = healthMarker.GetChild(1).localScale;
            //healthMarker.GetChild(1).localScale -= new Vector3(scale1.x, scale1.y, healthMarkerLength - healthMarkerLength * ((float)health / fullHealth));
        }
    }
}

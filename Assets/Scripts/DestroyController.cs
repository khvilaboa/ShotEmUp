using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour {

    private const int SCORE_ASTEROID = 10;

    private GameController gameController;
    public GameObject explosion;
    public int health = 100;

    void Awake() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Boundary") return;
        if (tag == "Enemy" && (coll.tag == "Enemy" || coll.tag == "EnemyShot")) return;
        if (tag == "Player" && coll.tag == "PlayerShot") return;

        

        if (coll.tag == "PlayerShot" || coll.tag == "EnemyShot") Destroy(coll.gameObject);
        Debug.Log("Fire");

        health -= 10;
        if (health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            
            if (tag == "Player") gameController.GameOver();
        }
    }
}

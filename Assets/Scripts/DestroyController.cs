using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour {

    private const int SCORE_ASTEROID = 10;

    private GameController gameController;
    public GameObject explosion;

    void Awake() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Boundary") return;
        if (tag == "Enemy" && (coll.tag == "Enemy" || coll.tag == "EnemyShot")) return;
        if (tag == "Player" && coll.tag == "PlayerShot") return;

        Instantiate(explosion, transform.position, transform.rotation);

        if (tag == "Player") gameController.GameOver();

        if (coll.tag == "PlayerShot" || coll.tag == "EnemyShot") Destroy(coll.gameObject);

        Destroy(gameObject);
    }
}

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
    private bool destroyed;

    void Awake() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        fullHealth = health;
        destroyed = false;
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

        if (tag == "Player" && coll.tag == "Item") {
            if (coll.name.StartsWith(GameController.ITEM_LIFE)) {
                health = Mathf.Min(health + GameController.ITEM_LIFE_POINTS, fullHealth);
                gameController.UpdateHealth((float) health / fullHealth);
            } else {
                gameController.activateItem(coll);
            }
            
            Destroy(coll.gameObject);
            return;
        }

        if (coll.tag == "PlayerShot" || coll.tag == "EnemyShot") Destroy(coll.gameObject);

        health = Mathf.Max(health - gameController.getDamageOf(coll.name), 0);
        if (tag == "Player") gameController.UpdateHealth((float) health / fullHealth);

        if (!destroyed && (health <= 0 || (tag == "Enemy" && coll.tag == "Player"))) {
            destroyed = true;
            if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
            
            if (tag == "Player") {
                gameController.GameOver();
            } else {
                gameController.EnemyDead(gameObject, true);
            }

            Destroy(gameObject);
        } else if (healthMarker != null) {
            float step = healthMarkerLength * (10f / fullHealth);
            healthMarker.GetChild(0).localScale -= new Vector3(0, step, 0);
            healthMarker.GetChild(0).position -= new Vector3(step, 0, 0);
            healthMarker.GetChild(1).localScale += new Vector3(0, step, 0);
            healthMarker.GetChild(1).position -= new Vector3(step, 0, 0);
        }
    }
}

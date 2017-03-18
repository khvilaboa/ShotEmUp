using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour {

    private GameController gameController;

    void Awake() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerExit(Collider coll)
    {
        Destroy(coll.gameObject);
        if (coll.tag != "EnemyShot" && coll.tag != "PlayerShot") gameController.EnemyDead(coll.gameObject, false);
        Debug.Log("Exit:" + coll);
    }
}

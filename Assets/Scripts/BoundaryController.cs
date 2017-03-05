using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour {

    void OnTriggerExit(Collider coll)
    {
        Destroy(coll.gameObject);
        Debug.Log("Exit:" + coll);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ShotController : MonoBehaviour {

    public GameObject shot;  // PreFab
    public Transform turrets;
    public bool alternateTurrets;
    private int nextTurret;  // Used when alternate is active

    void Start () {
        nextTurret = 0;
    }

    void LateUpdate()
    {
        if (GameOptions.areSoundEffectsEnabled == false)
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShot"))
            {
                go.GetComponent<AudioSource>().enabled = false;
            }
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("PlayerShot"))
            {
                go.GetComponent<AudioSource>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("EnemyShot"))
            {
                go.GetComponent<AudioSource>().enabled = true;
            }
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("PlayerShot"))
            {
                go.GetComponent<AudioSource>().enabled = true;
            }
        }
    }

    public void Fire()
    {
        if (turrets.childCount > 0)
        {
            if (alternateTurrets)
            {
                Instantiate(shot, turrets.GetChild(nextTurret).position, shot.transform.rotation);
                nextTurret = (nextTurret + 1) % turrets.childCount;
            }
            else {
                for (int i = 0; i < turrets.childCount; i++)
                {
                    Instantiate(shot, turrets.GetChild(i).position, shot.transform.rotation);
                }
            }
        }
    }

    public void FireTarget(GameObject go)
    {
        if (turrets.childCount > 0)
        {
            if (alternateTurrets)
            {
                GameObject clone = Instantiate(shot, turrets.GetChild(nextTurret).position, shot.transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = go.transform.position - turrets.GetChild(nextTurret).position;
                clone.transform.LookAt(go.transform);
                nextTurret = (nextTurret + 1) % turrets.childCount;
            }
            else {
                for (int i = 0; i < turrets.childCount; i++)
                {
                    GameObject clone = Instantiate(shot, turrets.GetChild(i).position, shot.transform.rotation);
                    clone.GetComponent<Rigidbody>().velocity = go.transform.position - turrets.GetChild(i).position;
                    clone.transform.LookAt(go.transform);
                }
            }
        }
    }


    public void Fire(int numTurret)
    {
        numTurret = Mathf.Clamp(numTurret, 0, turrets.childCount);
        Instantiate(shot, turrets.GetChild(numTurret).position, shot.transform.rotation);
    }

    public void Burst(float duration, bool inverse = false) {
        StartCoroutine(GenerateBurst(duration));
    }

    IEnumerator GenerateBurst(float duration)
    {
        float delay = (duration / turrets.childCount) * 1;
        for (int i = 0; i < turrets.childCount; i++)
        {
            Instantiate(shot, turrets.GetChild(i).position, shot.transform.rotation);
            yield return new WaitForSeconds(delay);
        }
    }
}

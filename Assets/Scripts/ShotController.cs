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
	
	public void FireAll () {
        if (turrets.childCount > 0) {
            if (alternateTurrets)
            {
                Instantiate(shot, turrets.GetChild(nextTurret).position, shot.transform.rotation);
                nextTurret = (nextTurret + 1) % turrets.childCount;
            } else {
                for (int i = 0; i < turrets.childCount; i++)
                {
                    Instantiate(shot, turrets.GetChild(i).position, shot.transform.rotation);
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

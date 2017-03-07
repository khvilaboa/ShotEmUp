using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private const string TEXT_SCORE = "Score: ";

    [Header("Asteroids")]
    public GameObject asteroid;
    public Vector3 asteriodRange;
    public int minAsteroidsPerWave;
    public int maxAsteroidsPerWave;
    public float asteroidDelay;
    public float timeBetweenWaves;

    [Header("UI")]
    public Text txtScore;
    private int score;

    void Awake() {
        score = 0;
    }

    void Start () {
        UpdateScore();
        StartCoroutine(GenerateAsteroids());
    }

    public void AddScore(int valueToAdd) {
        score += valueToAdd;
        UpdateScore();
    }

    void UpdateScore () {
        txtScore.text = TEXT_SCORE + score;
	}

    // Coroutine to generate asteroid waves
    IEnumerator GenerateAsteroids() {
        while(true)
        {
            int numAsteroids = Random.Range(minAsteroidsPerWave, maxAsteroidsPerWave);

            for (int i = 0; i < numAsteroids; i++)
            {
                float xPosition = Random.Range(-asteriodRange.x, asteriodRange.x);
                Vector3 asteroidPosition = new Vector3(xPosition, asteriodRange.y, asteriodRange.z);
                Instantiate(asteroid, asteroidPosition, Quaternion.identity);
                yield return new WaitForSeconds(asteroidDelay);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}

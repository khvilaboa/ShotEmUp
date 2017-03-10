using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Text txtGameOver;
    public Text txtRestart;
    private int score;
    private bool gameOver;

    void Awake() {
        score = 0;
        gameOver = false;
    }

    void Start () {
        UpdateScore();
        UpdateAsteroidLimit();

        // Hide game over texts
        txtGameOver.gameObject.SetActive(false);
        txtRestart.gameObject.SetActive(false);

        // Start spawning asteroid waves
        StartCoroutine(GenerateAsteroids());
    }

    void Update() {
        if(gameOver && Input.GetKey(KeyCode.R)) {
            txtGameOver.gameObject.SetActive(false);
            txtRestart.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int valueToAdd) {
        score += valueToAdd;
        UpdateScore();
    }

    void UpdateScore () {
        txtScore.text = TEXT_SCORE + score;
	}

    void UpdateAsteroidLimit() {
        Vector2 dims = Utils.GetViewDimensions();
        asteriodRange.x = dims.x / 2 - 4;
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
            if (gameOver) break;
        }
    }

    public void GameOver() {
        txtGameOver.gameObject.SetActive(true);
        txtRestart.gameObject.SetActive(true);
        gameOver = true;
    }
}

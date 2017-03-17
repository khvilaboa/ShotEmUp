using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private const string TEXT_SCORE = "Score: ";

    private const string ENEMY_ASTEROID = "asteroid";
    private const int ENEMY_ASTEROID_SCORE = 10;
    private const int ENEMY_ASTEROID_DAMAGE = 20;

    private const string ENEMY_WARSHIP_BLACK = "warshipBlack";
    private const int ENEMY_WARSHIP_BLACK_SCORE = 50;
    private const int ENEMY_WARSHIP_BLACK_DAMAGE = 50;

    private const string ENEMY_WARSHIP_GREY = "warshipGrey";
    private const int ENEMY_WARSHIP_GREY_SCORE = 50;
    private const int ENEMY_WARSHIP_GREY_DAMAGE = 50;

    private const string SHOT = "shot";
    private const int SHOT_DAMAGE = 10;

    private enum WaveType { Asteroid, Warship };

    [Header("Asteroids")]
    public GameObject[] asteroids;
    public Vector3 asteriodRange;
    public int minAsteroidsPerWave;
    public int maxAsteroidsPerWave;
    public float asteroidDelay;
    public float timeBetweenWaves;

    [Header("Enemies")]
    public GameObject[] warships;

    [Header("UI")]
    public Text txtScore;
    public Text txtGameOver;
    public Text txtRestart;
    public Transform healthMarker;

    [Header("Other")]
    public GameObject player;

    private int score;
    private bool gameOver;
    private int enemiesLeft = 0;

    void Awake()
    {
        score = 0;
        gameOver = false;
    }

    void Start()
    {
        UpdateScore();
        UpdateAsteroidLimit();

        // Hide game over texts
        txtGameOver.gameObject.SetActive(false);
        txtRestart.gameObject.SetActive(false);

        // Start spawning asteroid waves
        LaunchWave();
    }

    void Update()
    {
        if (gameOver && Input.GetKey(KeyCode.R))
        {
            txtGameOver.gameObject.SetActive(false);
            txtRestart.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int valueToAdd)
    {
        score += valueToAdd;
        UpdateScore();
    }

    void UpdateScore()
    {
        txtScore.text = TEXT_SCORE + score;
    }

    void UpdateAsteroidLimit()
    {
        Vector2 dims = Utils.GetViewDimensions();
        asteriodRange.x = dims.x / 2 - 4;
    }

    

    public void GameOver()
    {
        txtGameOver.gameObject.SetActive(true);
        txtRestart.gameObject.SetActive(true);
        gameOver = true;
    }

    public void UpdateHealth(float healthPercentage)
    {
        // Modify the green bar length
        Vector3 greenHealth = healthMarker.GetChild(0).localScale;
        healthMarker.GetChild(0).localScale = new Vector3(healthPercentage, greenHealth.y, greenHealth.z);

        // Modify the red bar length
        Vector3 redHealth = healthMarker.GetChild(1).localScale;
        healthMarker.GetChild(1).localScale = new Vector3(1 - healthPercentage, greenHealth.y, greenHealth.z);

        // Modifify the red bar position
        RectTransform redTransform = healthMarker.GetChild(1).GetComponent<RectTransform>();
        float redWidth = redTransform.sizeDelta.x;
        redTransform.anchoredPosition = new Vector3(redWidth * healthPercentage, redTransform.anchoredPosition.y);
    }

    public void EnemyDead(string enemyClass, bool destroyedByPlayer) {
        enemiesLeft -= 1;
        Debug.Log("enemies left: " + enemiesLeft);

        if(destroyedByPlayer) {
            if (enemyClass.StartsWith(ENEMY_WARSHIP_BLACK))
                AddScore(ENEMY_WARSHIP_BLACK_SCORE);
            else if (enemyClass.StartsWith(ENEMY_WARSHIP_GREY))
                AddScore(ENEMY_WARSHIP_GREY_SCORE);
            else if (enemyClass.StartsWith(ENEMY_ASTEROID))
                AddScore(ENEMY_ASTEROID_SCORE);
        }
        

        if (enemiesLeft == 0 && !gameOver) {
            LaunchWave();
        }
    }

    public void LaunchWave() {
        int rnd = Random.Range(1, 10);
        if(rnd < 8) {
            StartCoroutine(GenerateAsteroids());
        } else {
            enemiesLeft = 1;
            float xPosition = Random.Range(-asteriodRange.x, asteriodRange.x);
            Vector3 position = new Vector3(xPosition, asteriodRange.y, 100);
            GameObject enemy = warships[Random.Range(0,warships.Length)];
            GameObject enemyInst = Instantiate(enemy, position, Quaternion.identity);
            if(enemyInst.name.StartsWith(ENEMY_WARSHIP_GREY)) {
                enemyInst.GetComponent<FollowMovement>().target = player;
                enemyInst.GetComponent<ShotController>().shot.GetComponent<FollowMovement>().target = player;
            }
        }
        
    }

    // Coroutine to generate asteroid waves
    IEnumerator GenerateAsteroids() {
        int numAsteroids = Random.Range(minAsteroidsPerWave, maxAsteroidsPerWave);
        enemiesLeft += numAsteroids;

        for (int i = 0; i < numAsteroids; i++) {
            float xPosition = Random.Range(-asteriodRange.x, asteriodRange.x);
            Vector3 asteroidPosition = new Vector3(xPosition, asteriodRange.y, asteriodRange.z);
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], asteroidPosition, Quaternion.identity);
            yield return new WaitForSeconds(asteroidDelay);
        }
    }

    public int getDamageOf(string name) {
        if (name.StartsWith(ENEMY_WARSHIP_BLACK))
            return ENEMY_WARSHIP_BLACK_DAMAGE;
        else if (name.StartsWith(ENEMY_WARSHIP_GREY))
            return ENEMY_WARSHIP_GREY_DAMAGE;
        else if (name.StartsWith(ENEMY_ASTEROID))
            return ENEMY_ASTEROID_DAMAGE;
        else if (name.StartsWith(SHOT))
            return SHOT_DAMAGE;

        return 0;
    }
}

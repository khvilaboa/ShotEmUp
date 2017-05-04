using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private const string TEXT_SCORE = "Score: ";

    public const string ENEMY_ASTEROID = "asteroid";
    private const int ENEMY_ASTEROID_SCORE = 10;
    private const int ENEMY_ASTEROID_DAMAGE = 20;

    public const string ENEMY_WARSHIP_BLACK = "warshipBlack";
    private const int ENEMY_WARSHIP_BLACK_SCORE = 50;
    private const int ENEMY_WARSHIP_BLACK_DAMAGE = 50;

    public const string ENEMY_WARSHIP_GREY = "warshipGrey";
    private const int ENEMY_WARSHIP_GREY_SCORE = 50;
    private const int ENEMY_WARSHIP_GREY_DAMAGE = 50;

    public const string ENEMY_WARSHIP_RED = "warshipRed";
    private const int ENEMY_WARSHIP_RED_SCORE = 50;
    private const int ENEMY_WARSHIP_RED_DAMAGE = 50;

    public const string SHOT = "shot";
    private const int SHOT_DAMAGE = 10;

    public const string ITEM_LIFE = "itemLife";
    public const int ITEM_LIFE_POINTS = 25;

    public const string ITEM_SHOT_SPEED = "itemShotSpeed";
    private const int ITEM_SHOT_SPEED_DURATION = 10;
    private const float ITEM_SHOT_SPEED_MULT = 1.5f;
    private const int ITEM_SHOT_SPEED_ACUM = 3;  // Number of items of this type that can be accumulated
    private int shotSpeedCount = 0;  // Number of shot speed items currently activated

    public const string ITEM_BUDDY = "itemBuddy";
    public const int ITEM_BUDDY_HEALTH = 25;
    public GameObject buddyReference;

    //private enum WaveType { Asteroid, Warship };

    [Header("Asteroids")]
    public GameObject[] asteroids;
    public Vector3 asteroidRange;
    public int minAsteroidsPerWave;
    public int maxAsteroidsPerWave;
    public float asteroidDelay;

    [Header("Astras")]
    public GameObject[] astras;
    public int minAstrasPerWave;
    public int maxAstrasPerWave;
    public float astraDelay;
    public float startZ, stepZ;
    public int numStepsZ;

    [Header("Enemies")]
    public GameObject[] warships;

    [Header("Objects")]
    public GameObject[] oneTimeItems;
    public float itemSpawnProbability = 0.5f;
    public float itemAngularVelocity = 20;

    [Header("UI")]
    public Text txtScore;
    public Text txtGameOver;
    public Text txtRestart;
    public Transform healthMarker;

    [Header("Other")]
    public GameObject player;
    public float timeBetweenWaves;

    private int score;
    private bool gameOver;
    private int enemiesLeft = 0;

    void Awake()
    {
        score = 0;
        gameOver = false;
        buddyReference = null;
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
        asteroidRange.x = dims.x / 2 - 4;
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

    public void EnemyDead(GameObject enemy, bool destroyedByPlayer) {
        enemiesLeft -= 1;
        string enemyClass = enemy.name;
        Debug.Log("enemies left: " + enemiesLeft);

        if(destroyedByPlayer) {
            if (enemyClass.StartsWith(ENEMY_WARSHIP_BLACK))
                AddScore(ENEMY_WARSHIP_BLACK_SCORE);
            else if (enemyClass.StartsWith(ENEMY_WARSHIP_GREY))
                AddScore(ENEMY_WARSHIP_GREY_SCORE);
            else {  // Normal enemies (with probability of dropping items)
                if (enemyClass.StartsWith(ENEMY_ASTEROID))
                    AddScore(ENEMY_ASTEROID_SCORE);

                if(Random.Range(0, 100) < itemSpawnProbability * 100)
                    spawnRandomItem(enemy.transform.position);
            }
        }
        

        if (enemiesLeft == 0 && !gameOver) {
            LaunchWave();
        }
    }

    public void LaunchWave() {
        int rnd = Random.Range(1, 10);
        if (rnd < 4) {
            StartCoroutine(GenerateAsteroids());
        } else if (rnd < 6) {
            StartCoroutine(GenerateHorizontalAstras());
        } else {
            enemiesLeft = 1;
            float xPosition = Random.Range(-asteroidRange.x, asteroidRange.x);
            Vector3 position = new Vector3(xPosition, asteroidRange.y, 100);
            GameObject enemy = warships[Random.Range(0,warships.Length)];
            GameObject enemyInst = Instantiate(enemy, position, Quaternion.identity);
            if(enemyInst.name.StartsWith(ENEMY_WARSHIP_GREY)) {
                enemyInst.GetComponent<FollowMovement>().target = player;
                enemyInst.GetComponent<ShotController>().shot.GetComponent<FollowMovement>().target = player;
            }
            else if (enemyInst.name.StartsWith(ENEMY_WARSHIP_RED))
            {
                enemyInst.GetComponent<FollowMovement>().target = player;
                enemyInst.GetComponent<RedWarshipController>().target = player;
            }
        }
        
    }

    // Coroutine to generate asteroid waves
    IEnumerator GenerateAsteroids()
    {
        int numAsteroids = Random.Range(minAsteroidsPerWave, maxAsteroidsPerWave);
        enemiesLeft += numAsteroids;

        for (int i = 0; i < numAsteroids; i++)
        {
            float xPosition = Random.Range(-asteroidRange.x, asteroidRange.x);
            Vector3 asteroidPosition = new Vector3(xPosition, asteroidRange.y, asteroidRange.z);
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], asteroidPosition, Quaternion.identity);
            yield return new WaitForSeconds(asteroidDelay);
        }
    }

    // Coroutine to generate astra ships waves
    IEnumerator GenerateHorizontalAstras()
    {
        int numAstras = Random.Range(minAstrasPerWave, maxAstrasPerWave);
        enemiesLeft += numAstras * numStepsZ;
        GameObject astra = astras[Random.Range(0, astras.Length)];
        float xPosition, xVel = Mathf.Abs(astra.GetComponent<LinearMovement>().direction.x);
        int side = Random.Range(0, 2)==0?-1:1;

        for (int i = 0; i < numAstras; i++)
        {
            float zOff = startZ;
            float yStepCount = 0;

            while(yStepCount < numStepsZ)
            {
                xPosition = asteroidRange.x * (yStepCount % 2 == 0? -side : side);
                astra.GetComponent<LinearMovement>().direction.x = xVel * (yStepCount % 2 == 0 ? side : -side);
                Vector3 astraPosition = new Vector3(xPosition, asteroidRange.y, zOff);
                Instantiate(astra, astraPosition, Quaternion.Euler(new Vector3(0, 90, 0)));
                yStepCount += 1;
                zOff += stepZ;
            }

            yield return new WaitForSeconds(astraDelay);
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

    public void spawnRandomItem(Vector3 position) {
        GameObject item = Instantiate(oneTimeItems[Random.Range(0, oneTimeItems.Length)], position, Quaternion.identity);
        item.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, itemAngularVelocity ,0);
        item.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -10);
    }

    public void activateItem(Collider coll) {
        if (coll.name.StartsWith(ITEM_SHOT_SPEED))
            StartCoroutine(activateShotSpeedIncrement());  // Future use
        else if (coll.name.StartsWith(ITEM_BUDDY))
            activateBuddy();
    }

    private IEnumerator activateShotSpeedIncrement() {
        if (shotSpeedCount < ITEM_SHOT_SPEED_ACUM)
        {
            shotSpeedCount += 1;
            player.GetComponent<PlayerController>().fireRate /= ITEM_SHOT_SPEED_MULT;
            yield return new WaitForSeconds(ITEM_SHOT_SPEED_DURATION);
            player.GetComponent<PlayerController>().fireRate *= ITEM_SHOT_SPEED_MULT;
            shotSpeedCount -= 1;
        }
    }

    private void activateBuddy()
    {
        if (buddyReference == null)
        {
            buddyReference = Instantiate(player, player.transform.position + (new Vector3(0,0,50)), Quaternion.identity);
            buddyReference.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Destroy(buddyReference.GetComponent<PlayerController>());
            BuddyController buddyController = buddyReference.AddComponent<BuddyController>();

            ShotController shotController = buddyReference.GetComponent<ShotController>();
            buddyController.shotController = shotController;
            //shotController.shot.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            buddyController.GetComponent<DestroyController>().health = ITEM_BUDDY_HEALTH;

            OrbitantMovement orbitantMovement = buddyReference.AddComponent<OrbitantMovement>();
            orbitantMovement.center = player.transform;
            orbitantMovement.destroyController = buddyReference.GetComponent<DestroyController>();
        }
    }
}

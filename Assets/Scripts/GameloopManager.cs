using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameloopManager : MonoBehaviour
{
    public static GameloopManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static void SetState(GameState state)
    {
        instance.state = state;
    }

    public static void SpawnExplosionFX(Vector3 position)
    {
        GameObject explosionClone = Instantiate(instance.explosionPrefab, position, Quaternion.Euler(position));
        Destroy(explosionClone, 1f);
    }

    public enum GameState
    {
        MENU,
        GAMEPLAY,
        GAMEOVER,
    }
    public GameState state;

    [Header("Asteroid Variables")]
    public float asteroidSpawnCD;
    private float lastAsteroidSpawnTime;
    public GameObject asteroidPrefab;

    [Header("FX Variables")]
    public GameObject explosionPrefab;

    [Header("Score Variables")]
    public int highScore;
    public int currentScore;

    [Header("Gameplay Variables")]
    public int initialAsteroidCount;
    public GameObject playerPrefab;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        MenuManager.instance.UpdateHighscoreTexts(highScore);
    }

    private void Update()
    {
        if (GameloopManager.instance.state != GameloopManager.GameState.GAMEPLAY) return;

        // Asteroid spawn mechanics
        if (Time.time - lastAsteroidSpawnTime > asteroidSpawnCD)
            SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-5, 5),
            Random.Range(-5, 5),
            Random.Range(-5, 5)
            );

        Instantiate(asteroidPrefab, randomPos, Quaternion.Euler(randomPos), transform);

        lastAsteroidSpawnTime = Time.time;
    }

    public void IncreaseScore()
    {
        currentScore++;

        MenuManager.instance.UpdateScoreTexts(currentScore);

        if (instance.highScore < instance.currentScore)
        {
            PlayerPrefs.SetInt("highScore", instance.currentScore);
            MenuManager.instance.UpdateHighscoreTexts(currentScore);
        }
    }

    public void StartGame()
    {
        if (state == GameState.GAMEPLAY) return;

        // Asteroids setup
        DestroyAsteroids();
        SpawnInitialAsteroids();

        SetState(GameState.GAMEPLAY);
        MenuManager.instance.StartGame();

        currentScore = 0;
        MenuManager.instance.UpdateScoreTexts(currentScore);

        // Create new player if previous is destroyed
        if (PlayerController.instance == null)
        {
            Instantiate(instance.playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        if (state == GameState.GAMEOVER) return;

        SetState(GameState.GAMEOVER);
        if (state == GameState.GAMEOVER)
        {
            MenuManager.instance.GameOver();
            Invoke("DestroyAsteroids", 1f);
        }
    }

    private void DestroyAsteroids()
    {
        if (state == GameState.GAMEPLAY) return;

        AsteroidController[] asteroids = GetComponentsInChildren<AsteroidController>();
        for (int i = 0; i < asteroids.Length; i++)
        {
            SpawnExplosionFX(asteroids[i].transform.position);
            Destroy(asteroids[i].gameObject);  
        }
    }

    private void SpawnInitialAsteroids()
    {
        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }
}

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

    void Start()
    {
        state = GameState.GAMEPLAY;
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

    private void Update()
    {
        if (GameloopManager.instance.state != GameloopManager.GameState.GAMEPLAY) return;

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

        GameObject asteroidClone = Instantiate(asteroidPrefab, randomPos, Quaternion.Euler(randomPos));

        lastAsteroidSpawnTime = Time.time;
    }

    private void SpawnExplosion(Vector3 pos)
    {
        
    }
}

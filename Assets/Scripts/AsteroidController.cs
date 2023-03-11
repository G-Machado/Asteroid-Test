using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject splitPrefab;
    public int splitCount;
    public float randomVelRange;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 newRandomVel = new Vector2(Random.Range(-randomVelRange, randomVelRange + .01f),
                Random.Range(-randomVelRange, randomVelRange + .01f));
        rb.velocity = newRandomVel;

        rb.AddTorque(newRandomVel.magnitude * 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameloopManager.instance.state != GameloopManager.GameState.GAMEPLAY) return;

        if(collision.tag == "shot")
        {
            // Explosion effect
            GameloopManager.SpawnExplosionFX(collision.transform.position);

            SplitAsteroid();
            GameloopManager.instance.IncreaseScore();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Player")
        {
            // Explosion effects
            for (int i = 0; i < 10; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-.7f, .7f),
                    Random.Range(-.7f, .7f),
                    Random.Range(-.7f, .7f)
                );
                GameloopManager.SpawnExplosionFX(collision.transform.position + randomOffset);
            }

            SplitAsteroid();
            GameloopManager.instance.GameOver();
            Destroy(collision.gameObject);
        }
    }

    private void SplitAsteroid()
    {
        if (splitPrefab)
        {
            for (int i = 0; i < splitCount; i++)
            {
                GameObject splitClone = Instantiate(splitPrefab, transform.position, transform.rotation, GameloopManager.instance.transform);

                Vector2 newRandomVel = new Vector2(Random.Range(-randomVelRange, randomVelRange + .01f),
                    Random.Range(-randomVelRange, randomVelRange + .01f));
                splitClone.GetComponent<Rigidbody2D>().velocity = newRandomVel;
            }
        }

        Destroy(this.gameObject);
    }
}

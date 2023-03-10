using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject splitPrefab;
    public int splitCount;
    public float randomVelRange;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 newRandomVel = new Vector2(Random.Range(-randomVelRange, randomVelRange + .01f),
                Random.Range(-randomVelRange, randomVelRange + .01f));
        rb.velocity = newRandomVel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "shot")
        {
            SplitAsteroid();
        }
    }

    private void SplitAsteroid()
    {
        if (splitPrefab)
        {
            for (int i = 0; i < splitCount; i++)
            {
                GameObject splitClone = Instantiate(splitPrefab, transform.position, transform.rotation);

                Vector2 newRandomVel = new Vector2(Random.Range(-randomVelRange, randomVelRange + .01f),
                    Random.Range(-randomVelRange, randomVelRange + .01f));
                splitClone.GetComponent<Rigidbody2D>().velocity = newRandomVel;
            }
        }

        Destroy(this.gameObject);
    }
}

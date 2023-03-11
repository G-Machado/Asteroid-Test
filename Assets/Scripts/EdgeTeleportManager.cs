using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTeleportManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "topBarrier" && rb.velocity.y > 0)
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
        }
        else if (collision.tag == "botBarrier" && rb.velocity.y < 0)
        {
            transform.position =
                new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
        }
        else if (collision.tag == "leftBarrier" && rb.velocity.x < 0)
        {
            transform.position =
                new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
        else if (collision.tag == "rightBarrier" && rb.velocity.x > 0)
        {
            transform.position =
                new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
    }
}

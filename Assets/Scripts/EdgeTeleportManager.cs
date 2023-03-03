using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTeleportManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
        Debug.Log(collision.attachedRigidbody.transform.name);
    }
}

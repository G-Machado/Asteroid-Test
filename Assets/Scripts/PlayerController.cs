using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public float rotateSpeed;
    public float accelerationSpeed;
    public float maxVelocity;
    public Vector3 targetVelocity;
    [Range(0, 1)]
    public float velocityDamp = .1f;
    private Rigidbody2D rb;

    [Header("Shot Variables")]
    public GameObject shotPrefab;
    public float shotCD;
    public float shotSpeed;
    private float lastShotTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        Accelerate(verticalAxis);

        float horizontalAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(verticalAxis) < .5f)
            Rotate(horizontalAxis);

        if (Input.GetKey(KeyCode.Space) && Time.time - lastShotTime > shotCD)
            Shot();
    }

    private void Rotate(float input)
    {
        rb.MoveRotation(transform.eulerAngles.z + rotateSpeed * input  * -100f * Time.deltaTime);
    }

    private void Accelerate(float input)
    {
        if (Mathf.Abs(input) > .05f)
        {
            targetVelocity = Vector3.ClampMagnitude(targetVelocity +
                 accelerationSpeed * transform.up, maxVelocity);
        }
        else
        {
            targetVelocity *= .99f;
        }

        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, 1 - velocityDamp);
    }

    private void Shot()
    {
        GameObject shotClone = Instantiate(shotPrefab, transform.position,
            Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90));
        shotClone.GetComponent<Rigidbody2D>().velocity = shotClone.transform.right * -shotSpeed;

        lastShotTime = Time.time;

        Destroy(shotClone, 3f);
    }
}

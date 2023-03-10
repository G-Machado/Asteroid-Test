using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Movement Variables")]
    public float rotateSpeed;
    public float accelerationSpeed;
    public float maxVelocity;
    public Vector3 targetVelocity;
    [Range(0, 1)]
    public float velocityDamp = .1f;
    private Rigidbody2D rb;
    public GameObject propulsionFX;

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
        if (GameloopManager.instance.state != GameloopManager.GameState.GAMEPLAY) return;

        // Movement mechanics
        float verticalAxis = Input.GetAxis("Vertical");
        Accelerate(verticalAxis);

        // Rotation mechanics
        float horizontalAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(verticalAxis) < .5f)
            Rotate(horizontalAxis);

        // Shot mechanics
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
                 accelerationSpeed * transform.up * Time.deltaTime, maxVelocity);

            propulsionFX.SetActive(true);
        }
        else
        {
            targetVelocity *= .99f;
            propulsionFX.SetActive(false);
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

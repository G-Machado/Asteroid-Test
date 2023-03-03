using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rotateSpeed;
    public float accelerationSpeed;
    public float maxVelocity;
    public Vector3 targetVelocity;
    [Range(0, 1)]
    public float velocityDamp = .1f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        Accelerate(verticalAxis);

        float horizontalAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(verticalAxis) < .5f)
            Rotate(horizontalAxis);
    }

    private void Rotate(float input)
    {
        rb.MoveRotation(transform.eulerAngles.z + rotateSpeed * input  * -100f * Time.deltaTime);
    }

    private void Accelerate(float input)
    {
        if (Mathf.Abs(input) > .1f)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for horizontal (X-axis) and vertical (Y-axis)
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical"); // Use Vertical for Y-axis movement

        // Calculate velocity based on input
        float xSpeed = xInput * speed;
        float ySpeed = yInput * speed;

        // Set the new velocity for the Rigidbody
        Vector3 newVelocity = new Vector3(xSpeed, ySpeed, 0f);
        playerRigidbody.velocity = newVelocity;
    }
}

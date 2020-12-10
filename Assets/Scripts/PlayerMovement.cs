using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2;
    public Transform cam;
    public Rigidbody rb;
    public float distanceToGround = 5f;
    bool isGrounded = true;
    public float turnRate = 7f;

    // Reset jumpCount to 0 when player it the ground

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Jumpable")
        {
            isGrounded = true;
        }
    }
    void FixedUpdate()
    {
        HandleMovementInput();
        HandleRotationInput();
        HandleShootInput();
    }

    void HandleMovementInput()
    {
        // Get the input from keyboard 
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        // Get the normalized vector for forward axis (z axis) and right axis (x axis)
        Vector3 camFoward = cam.forward.normalized;
        Vector3 camRight = cam.right.normalized;

        // Move player
        transform.position += (camFoward*input.y + camRight*input.x) * Time.deltaTime * moveSpeed;

        // Player is falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier -1) * Time.deltaTime; 
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Player is during the jump and we are not holding down jump button
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (isGrounded && Input.GetButton("Jump"))
        {
            isGrounded = false;
            rb.velocity = Vector3.up * jumpForce;
        }
    }

    void HandleRotationInput()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnRate * Time.deltaTime );
        }
    }

    void HandleShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            // Shoot
            PlayerGun.Instance.Shoot();
        }
    }
}

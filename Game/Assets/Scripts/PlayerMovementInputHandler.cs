using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovementInputHandler : NetworkBehaviour
{
    public Rigidbody playerRb;

    public Transform lowerBound;

    public CameraInputHandler cameraInputHandler;

    public float speed;

    public float jumpForce;

    public float rotationSpeed;

    public float isTouchingDist;

    public bool isTouching;

    public float isStickingDist;

    public bool isSticking;

    public Vector3 hitNormal;

    public Rigidbody hitRb;

    void Update()
    {
        if (Physics.Raycast(lowerBound.position, -lowerBound.up, out RaycastHit hitInfo, Mathf.Max(isTouchingDist, isStickingDist), LayerMask.GetMask("Sticky Surface", "Walkable Surface")))
        {
            isTouching = hitInfo.distance < isTouchingDist;
            isSticking = (hitInfo.distance < isStickingDist) && (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Sticky Surface"));
            hitNormal  = hitInfo.normal; 
            hitRb      = hitInfo.rigidbody;
        }
        else
        {
            isTouching = isSticking = false;
            hitNormal  = Vector3.zero;
            hitRb      = null;
        }

        if (!isSticking)
        {
            hitNormal = -Physics.gravity.normalized;
        }

        Quaternion rot;

        {
            GameObject playerBody = playerRb.gameObject;

            float angle = Vector3.Angle(playerBody.transform.up, hitNormal);

            Vector3 axis;
            if (Mathf.Approximately(angle, 180.0f))
            {
                axis = Quaternion.Euler(0.0f, 0.0f, 90.0f) * playerBody.transform.up;
            }
            else
            {
                axis = Vector3.Cross(playerBody.transform.up, hitNormal);
            }

            rot = Quaternion.AngleAxis(angle, axis) * playerBody.transform.rotation;

            playerBody.transform.rotation = Quaternion.AngleAxis(angle * rotationSpeed * Time.deltaTime, axis) * playerBody.transform.rotation;
        }

        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 fw, ri;

            {
                Quaternion tempQ = rot * Quaternion.Euler(0.0f, cameraInputHandler.sumX, 0.0f);
                fw = tempQ * Vector3.forward * vertical;
                ri = tempQ * Vector3.right * horizontal;
            }

            Vector3 newVel = (fw + ri).normalized * speed + hitNormal * Vector3.Dot(hitNormal, playerRb.velocity);

            if (Input.GetButtonDown("Jump") && isTouching)
            {
                if (isSticking)
                {
                    newVel += hitNormal * jumpForce;
                }
                else
                {
                    newVel += -Physics.gravity.normalized * jumpForce;
                }
            }

            playerRb.velocity = newVel;
        }
    }

    void FixedUpdate()
    {
        if (isSticking)
        {
            playerRb.AddForce(-Physics.gravity, ForceMode.Acceleration);
            playerRb.AddForce(-hitNormal * Physics.gravity.magnitude, ForceMode.Acceleration);
        }
    }
}

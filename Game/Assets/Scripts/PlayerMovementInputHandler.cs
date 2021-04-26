using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInputHandler : MonoBehaviour
{
    public Transform lowerBound;
    
    public bool isTouchingGround;

    public float groundCheckDistance = 0.1f;

    void Update()
    {
        // Update isTouchingGround
        {
            if (Physics.Raycast(lowerBound.position, -lowerBound.up, out RaycastHit hitInfo))
            {
                isTouchingGround = hitInfo.distance < groundCheckDistance;
            }
        }
    }
}

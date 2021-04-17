using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranslation : MonoBehaviour
{
    [Header("Components needed.")]
    public PlayerHandler handler;
    public Rigidbody rb;
    public Transform yRotObj;

    [Header("Player constants.")]
    public float moveSpeed = 1f;
    public float airSpeed = 1f;
    public float maxAirVel = 1f;

    void Awake()
    {
        if (handler is null || rb is null || yRotObj is null)
            Debug.LogError(new NullReferenceException("in PlayerTranslation"));
    }

    public void CustomUpdate()
    {
        Vector3 dir = yRotObj.forward * Input.GetAxisRaw("Vertical") + yRotObj.right * Input.GetAxisRaw("Horizontal");

        if (dir.magnitude > 0.5f)
            dir = dir.normalized;

        if (handler.isDown)
            rb.velocity = dir * moveSpeed;
        else if (rb.velocity.magnitude < maxAirVel)
            rb.AddForce(dir * airSpeed, ForceMode.Acceleration);
    }
}

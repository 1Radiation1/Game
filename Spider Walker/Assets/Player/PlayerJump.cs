using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Components needed.")]
    public PlayerHandler handler;
    public Transform upRotObj;
    public Rigidbody rb;

    [Header("Player constants.")]
    public float jumpVel = 1f;

    void Awake()
    {
        if (handler is null || upRotObj is null || rb is null)
            Debug.LogException(new NullReferenceException("in PlayerJump"));
    }

    public void CustomUpdate()
    {
        if (handler.isDown && Input.GetAxisRaw("Jump") > 0.5f)
        {
            Vector3 relVel = upRotObj.InverseTransformDirection(rb.velocity);
            relVel.y = jumpVel;
            rb.velocity = upRotObj.TransformDirection(relVel);
        }
    }
}

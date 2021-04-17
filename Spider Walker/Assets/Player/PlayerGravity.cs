using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [Header("Components needed.")]
    public PlayerHandler handler;
    public Transform startP;
    public Transform canJumpEP;
    public Transform canStickEP;
    public Transform upRotObj;
    public Rigidbody rb;

    [Header("Player constants.")]
    public float upAdjustTime = 1f;

    Transform lastGround;
    Quaternion fromInterp, toInterp;
    float t;

    void Awake()
    {
        if (handler is null || startP is null || canJumpEP is null || canStickEP is null || upRotObj is null || rb is null)
            Debug.LogException(new NullReferenceException("in PlayerGravity"));
    }

    public void CustomUpdate()
    {
        // For isDown:
        Vector3 delta = canJumpEP.position - startP.position;
        handler.isDown = Physics.Raycast(startP.position, delta, delta.magnitude, (1 << 10));

        // For gravity direction:
        delta = canStickEP.position - startP.position;
        Vector3 up = Vector3.up;
        Transform newGround = null;

        if (Physics.Raycast(startP.position, delta, out RaycastHit hitInfo, delta.magnitude, (1 << 10)))
        {
            newGround = hitInfo.transform;
            up = newGround.forward;
        }

        if (newGround != lastGround)
        {
            t = 0;
            fromInterp = upRotObj.localRotation;
            toInterp = upRotObj.localRotation * Quaternion.FromToRotation(upRotObj.up, up);
            lastGround = newGround;
        }
        else
            t = Mathf.Clamp01(t + Time.deltaTime / upAdjustTime);

        upRotObj.localRotation = Quaternion.Slerp(fromInterp, toInterp, t);
    }

    void FixedUpdate()
    {
        Vector3 delta = canJumpEP.position - startP.position, down = Vector3.down;

        if (Physics.Raycast(startP.position, delta, out RaycastHit hitInfo, delta.magnitude, (1 << 10)))
            down = -hitInfo.transform.forward;

        rb.AddForce(down * Physics.gravity.magnitude, ForceMode.Acceleration);
    }
}

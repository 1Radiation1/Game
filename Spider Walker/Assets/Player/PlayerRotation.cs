using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Components needed.")]
    public Transform yRotObj;
    public Transform xRotObj;

    [Header("Player constants.")]
    public float sensitivity = 1f;

    // Private values.
    float xRot = 0f, yRot = 0f;

    void Awake()
    {
        if (yRotObj is null || xRotObj is null)
            Debug.LogException(new NullReferenceException("in PlayerRotation"));
    }

    public void CustomUpdate()
    {
        float deltaY = Input.GetAxis("Mouse X") * sensitivity;
        float deltaX = -Input.GetAxis("Mouse Y") * sensitivity;

        yRot = Mathf.Repeat(yRot + deltaY, 360f);
        yRotObj.localRotation = Quaternion.Euler(0f, yRot, 0f);

        xRot = Mathf.Clamp(xRot + deltaX, -90f, 90f);
        xRotObj.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}

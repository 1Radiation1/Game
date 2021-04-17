using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Header("Components needed.")]
    public PlayerGravity gravScript;
    public PlayerTranslation moveScript;
    public PlayerRotation rotScript;
    public PlayerJump jumpScript;

    [Header("Player data.")]
    public bool isDown;

    void Awake()
    {
        if (gravScript is null || moveScript is null || rotScript is null || jumpScript is null)
            Debug.LogError(new NullReferenceException("in PlayerHandler"));
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        gravScript.CustomUpdate();
        moveScript.CustomUpdate();
        rotScript.CustomUpdate();
        jumpScript.CustomUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    public Camera playerCamera;

    public GameObject movementBody;

    void Start()
    {
        if (!isLocalPlayer)
        {
            playerCamera.enabled = false;
            {
                var rb = movementBody.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.detectCollisions = rb.useGravity = false;
            }
            {
                var colls = movementBody.GetComponents<Collider>();
                foreach (var coll in colls)
                {
                    coll.enabled = false;
                }
            }
            {
                var pmihs = GetComponents<PlayerMovementInputHandler>();
                foreach (var pmih in pmihs)
                {
                    pmih.enabled = false;
                }
            }
        }
    }
}

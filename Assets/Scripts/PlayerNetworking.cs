using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class PlayerNetworking : PlayerBehavior
{
    private Rigidbody2D rbody;

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
        {
            return;
        }
        if (rbody == null)
        {
            rbody = this.GetComponent<Rigidbody2D>();
            networkObject.UpdateInterval = 1;
        }

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            rbody.velocity = networkObject.velocity;
            rbody.rotation = networkObject.rotvelocity;
            return;
        }

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
        networkObject.velocity = rbody.velocity;
        networkObject.rotvelocity = rbody.rotation;
    }
}

using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class FlixNetworking : FlixBehavior
{
    private Rigidbody2D rbody;
    private float speed = 5f;

    public override void Explode(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        networkObject.UpdateInterval = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null)
        {
            return;
        }

        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            rbody.velocity = networkObject.velocity;
            rbody.rotation = networkObject.rotvelocity;
            return;
        }

        rbody.velocity += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed * Time.deltaTime;

        networkObject.position = transform.position;
        networkObject.rotation    = transform.rotation;
        networkObject.velocity    = rbody.velocity; 
        networkObject.rotvelocity = rbody.rotation;    
    }
}

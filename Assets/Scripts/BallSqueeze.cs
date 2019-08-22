using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSqueeze : MonoBehaviour {
    private Rigidbody2D myBody;
    public float SqueezeFactor = .3f;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += myBody.velocity * myBody.mass * SqueezeFactor;
            var yourRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            myBody.velocity += yourRigidbody.velocity * yourRigidbody.mass * SqueezeFactor;
        }
    }
}

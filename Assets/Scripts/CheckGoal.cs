using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGoal : MonoBehaviour {
    public Transform RedGoal;
    public Transform BlueGoal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Goal")
            return;
        var trail = this.GetComponent<TrailRenderer>();
        trail.Clear();
        var t = trail.time;
        trail.time = 0.01f;
        trail.Clear();
        //trail.emitting= false;
        //trail.enabled = false;
        this.transform.position = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(BlueGoal, collision.transform.position, Quaternion.identity);
        //trail.enabled = true;
        //trail.emitting = true;
        trail.time = t;
        trail.Clear();
    }
}

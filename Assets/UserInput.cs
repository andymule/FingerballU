using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {
    public GameObject myBall;
	
	void Update () {
        Vector2 diff = Vector2.zero;
        if (Input.touchCount > 0)
        {
            var touchpoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            diff = touchpoint - myBall.transform.position ;
        }
        if (Input.GetMouseButton(0))
        {
            var touch = Input.mousePosition;
            var touchpoint = Camera.main.ScreenToWorldPoint(touch);
            diff = touchpoint - myBall.transform.position ;
        }
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            myBall.GetComponent<Rigidbody2D>().AddForce(diff);
        }
    }
}

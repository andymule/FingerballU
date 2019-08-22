using Mirror;
using UnityEngine;

public class UserInput : NetworkBehaviour //MonoBehaviour
{
    private GameObject myBall;
    public float Sensitivity = 20f;
    public float MaxAccel = 2f;
    public float MaxSpeed = 20f;

    private float boostAccel = 5f;
    private bool isBoosting = false;


    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        myBall = gameObject;
        Input.multiTouchEnabled = true;
    }
    private int mainFingerID = -1;
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector2 diff = Vector2.zero;
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            mainFingerID = Input.GetTouch(0).fingerId;
        }
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == mainFingerID)
                {
                    Vector3 touchpoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    diff = touchpoint - myBall.transform.position;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 touch = Input.mousePosition;
            Vector3 touchpoint = Camera.main.ScreenToWorldPoint(touch);
            diff = touchpoint - myBall.transform.position;
        }

        if (Input.touchCount > 1 || Input.GetKey(KeyCode.Space))
        {
            isBoosting = true;
            myBall.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            myBall.transform.GetChild(0).GetComponent<TrailRenderer>().emitting = true;
        }
        else
        {
            isBoosting = false;
            myBall.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            myBall.transform.GetChild(0).GetComponent<TrailRenderer>().emitting = false;
        }

        if (diff != Vector2.zero)
        {
            diff *= Sensitivity;
            if (isBoosting)
            {
                diff = Vector3.ClampMagnitude(diff, boostAccel);
            }
            else
            {
                diff = Vector3.ClampMagnitude(diff, MaxAccel);
            }

            myBall.GetComponent<Rigidbody2D>().AddForce(diff);
        }
    }
}

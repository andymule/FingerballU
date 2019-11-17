using System.Collections;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{
    public Transform RedGoal;
    public Transform BlueGoal;
    private TrailRenderer trail => GetComponent<TrailRenderer>();
    private CircleCollider2D collider => GetComponent<CircleCollider2D>();
    private Renderer renderer => GetComponent<Renderer>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Goal")
        {
            return;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (collision.gameObject.name == "BlueGoal")
        {
            Instantiate(RedGoal, collision.transform.position, Quaternion.identity); // show other team color
        }
        else
        {
            Instantiate(BlueGoal, collision.transform.position, Quaternion.identity);
        }

        trail.emitting = false;
        renderer.enabled = false;
        collider.enabled = false;
        transform.position = Vector3.zero;
        trail.Clear();
        SlowTime();
        StartCoroutine(ResetLevel());
    }

    private void SlowTime()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(1.5f);
        trail.emitting =   true;
        renderer.enabled = true;
        collider.enabled = true;
        SlowTime();
    }
}

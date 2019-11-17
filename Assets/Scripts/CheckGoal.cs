using System.Collections;
using UnityEngine;

public class CheckGoal : MonoBehaviour
{
    public Transform RedGoal;
    public Transform BlueGoal;
    private TrailRenderer trail => GetComponent<TrailRenderer>();
    private CircleCollider2D collider => GetComponent<CircleCollider2D>();
    private Renderer renderer => GetComponent<Renderer>();
    public Transform camTransform => Camera.main.transform;

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
        //StartCoroutine(ShakeCamera());
        StartCoroutine(ResetLevel());
    }

    private IEnumerator ShakeCamera()
    {
        // How long the object should shake for.
        float shakeDuration = 1f;

        // Amplitude of the shake. A larger value shakes the camera harder.
        float shakeAmount = 0.7f;
        float decreaseFactor = 1.0f;

        Vector3 originalPos = camTransform.position;

        {
            if (shakeDuration > 0)
            {
                camTransform.position = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                camTransform.position = originalPos;
            }
            yield return null;
        }
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
        trail.emitting = true;
        renderer.enabled = true;
        collider.enabled = true;
        SlowTime();
    }
}

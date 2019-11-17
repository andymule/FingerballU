using System.Collections.Generic;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    public int count = 100;
    public float RespawnDistance = 25f;
    public GameObject StarPrefab;
    public GameObject StartPosition;
    List<GameObject> stars;
    private List<GameObject> starsToRespawn;
    private Dictionary<int, Vector2> starSpeed; // lets manually do star velocity bc y not
    public Vector2 moveVelocity = new Vector3(.03f, .06f);
    [Range(0f, 1f)]
    public float starVelDepthSpeedVariation = .03f;
    public bool rotateStars = true; // slowly change background
    private float rotateStepMax = .04f;

    void Start()
    {
        stars = new List<GameObject>(count);
        starsToRespawn = new List<GameObject>(count);
        starSpeed = new Dictionary<int, Vector2>();

        for (int i = 0; i < stars.Capacity; i++)
        {
            Vector3 randoPos = new Vector3(Random.value * 30 - 15f, Random.value * 30f - 15f, 0);
            randoPos += StartPosition.transform.localPosition / 2;
            GameObject newStar = Instantiate(StarPrefab, randoPos, Quaternion.identity);
            newStar.transform.parent = transform;
            stars.Add(newStar);
            float v = (Random.value * starVelDepthSpeedVariation);
            Vector2 randoSpeed = new Vector2(v + moveVelocity.x, v + moveVelocity.y);
            starSpeed[i] = randoSpeed;
        }
    }

    void FixedUpdate()
    {
        if (rotateStars)
        {
            transform.Rotate(Vector3.forward, rotateStepMax * Random.value); // spin background
        }

        for (int i = 0; i < stars.Capacity; i++) // velocity update
        {
            stars[i].transform.localPosition += (Vector3)starSpeed[i] * Time.timeScale;
            if (Vector2.Distance(stars[i].transform.localPosition, StartPosition.transform.localPosition) > RespawnDistance)
            {
                starsToRespawn.Add(stars[i]);
            }
        }
        foreach (GameObject g in starsToRespawn) // move stars back to spawn point if too far, randomize placement
        {
            Vector3 randomNudge = Random.insideUnitCircle;
            randomNudge = new Vector2(randomNudge.x * StartPosition.transform.localScale.x / 2, randomNudge.y * StartPosition.transform.localScale.z / 2);
            g.transform.localPosition = StartPosition.transform.localPosition + randomNudge;
        }
        starsToRespawn.Clear();
    }
}

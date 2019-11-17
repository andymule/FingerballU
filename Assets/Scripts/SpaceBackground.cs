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
    public Vector2 moveVelocity = new Vector3(.01f, .013f);
    [Range(0f, 1f)]
    public float starVelDepthSpeedVariation = .01f;
    public bool rotateStars = true; // slowly change background
    private float rotateStepMax = .1f;

    void Start()
    {
        stars = new List<GameObject>(count);
        starsToRespawn = new List<GameObject>(count);
        starSpeed = new Dictionary<int, Vector2>();

        for (int i = 0; i < stars.Capacity; i++)
        {
            Vector3 randoPos = new Vector3(Random.value * 30 - 15f, Random.value * 30f - 15f, 0);
            randoPos += StartPosition.transform.localPosition / 2;
            var newStar = Instantiate(StarPrefab, randoPos, Quaternion.identity);
            newStar.transform.parent = this.transform;
            stars.Add(newStar);
            float v = (Random.value * starVelDepthSpeedVariation);
            Vector2 randoSpeed = new Vector2(v + moveVelocity.x, v + moveVelocity.y);
            starSpeed[i] = randoSpeed;
        }
    }

    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotateStepMax * Random.value); // spin background

        for (int i = 0; i < stars.Capacity; i++)
        {
            stars[i].transform.localPosition += (Vector3)starSpeed[i];
            if (Vector2.Distance(stars[i].transform.localPosition, StartPosition.transform.localPosition) > RespawnDistance)
            {
                starsToRespawn.Add(stars[i]);
            }
        }
        foreach (GameObject g in starsToRespawn)
        {
            Vector3 randomNudge = Random.insideUnitCircle;
            randomNudge = new Vector2(randomNudge.x * StartPosition.transform.localScale.x / 2, randomNudge.y * StartPosition.transform.localScale.z / 2);
            g.transform.localPosition = StartPosition.transform.localPosition + randomNudge;
        }
        starsToRespawn.Clear();
    }
}

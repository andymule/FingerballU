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

    void Start()
    {
        stars = new List<GameObject>(count);
        starsToRespawn = new List<GameObject>(count);
        for (int i = 0; i < stars.Capacity; i++)
        {
            Vector3 rando = new Vector3(Random.value * 30 - 15f, Random.value * 30f - 15f, 0);
            rando += StartPosition.transform.position/2;
            stars.Add(Instantiate(StarPrefab, rando, Quaternion.identity));
        }
    }

    void Update()
    {
        foreach (GameObject g in stars)
        {
            g.transform.position += (Vector3)moveVelocity;
            if (Vector2.Distance(g.transform.position, StartPosition.transform.position) > RespawnDistance)
            {
                starsToRespawn.Add(g);
            }
        }
        foreach (GameObject g in starsToRespawn)
        {
            Vector3 randomNudge = Random.insideUnitCircle;
            randomNudge = new Vector2(randomNudge.x * StartPosition.transform.localScale.x/2, randomNudge.y * StartPosition.transform.localScale.z/2);
            g.transform.position = StartPosition.transform.position+ randomNudge;
        }
        starsToRespawn.Clear();
    }
}

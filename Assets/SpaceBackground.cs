using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    public int count = 100;
    public GameObject StarPrefab;
    List<GameObject> stars = new List<GameObject>(50);
    Vector3 moving = new Vector3(.01f, .013f, 0);
    void Start()
    {
        for (int i=0; i<stars.Capacity; i++)
        {
            Vector3 rando = new Vector3(Random.value * 20f - 10f, Random.value * 20f - 10f, 0);
            stars.Add(Instantiate(StarPrefab, rando, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in stars)
        {
            g.transform.position += moving;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZ : MonoBehaviour
{

    void Update()
    {
        var p = this.transform.position;
        this.transform.position = new Vector3(p.x, p.y, 0);
    }
}

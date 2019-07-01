using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    [Range(0, 1)]
    public float lerp;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) transform.position = Vector3.Lerp(transform.position, target.transform.position, lerp) + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    [Range(0, 1)]
    public float bias = .33333f;
    public Vector3 offset;

    public bool fixedUpdate;

    // Start is called before the first frame update
    void Update()
    {
        if (!fixedUpdate) UpdatePosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fixedUpdate) UpdatePosition();
    }


    void UpdatePosition()
    {
        if (target != null) transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, bias);
    }
}

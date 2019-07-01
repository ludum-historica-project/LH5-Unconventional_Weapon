using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjectile : MonoBehaviour
{
    public float speed = 5;

    public System.Action<Collision2D> OnCollision2D = (coll) => { };


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision2D(collision);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

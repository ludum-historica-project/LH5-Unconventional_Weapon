using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;

    Vector3 destination;

    Rigidbody2D _rb2d;
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector3 destination)
    {
        this.destination = destination;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (destination - transform.position).normalized;
        Vector3 movement = direction * speed;
        _rb2d.velocity = Vector3.Slerp(_rb2d.velocity, movement, .33333f);
    }
}

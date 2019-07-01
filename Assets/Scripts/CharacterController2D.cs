using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterController2D : MonoBehaviour
{
    public float speed = 5;


    private Vector2 _velocity;


    private Rigidbody2D _rb2d;
    private GrappleSystem _grappleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _grappleSystem = GetComponent<GrappleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb2d.AddForce(Vector2.right * Input.GetAxis("Horizontal") * speed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public float speed = 5;
    //public float rotationSpeed = 180;
    Rigidbody2D _rb2d;
    //float rotation = 0;


    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction.magnitude > 1) direction.Normalize();
        transform.up = direction;
        _rb2d.velocity = direction * speed;
        //rotation = _rb2d.rotation;
        //rotation += rotationSpeed * -Input.GetAxisRaw("Horizontal") * Director.GetManager<TimeManager>().fixedDeltaTime;

        //_rb2d.SetRotation(Vector2.SignedAngle(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position));
    }

}

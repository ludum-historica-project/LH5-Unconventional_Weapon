using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 180;
    Rigidbody2D _rb2d;
    float rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb2d.velocity = (transform.up * Input.GetAxis("Vertical") * speed);// + Vector3.right * Input.GetAxis("Horizontal") * speed);
        //if (_rb2d.velocity.magnitude > speed) _rb2d.velocity = _rb2d.velocity.normalized * speed;
        rotation = _rb2d.rotation;
        rotation += rotationSpeed * -Input.GetAxis("Horizontal") * Director.GetManager<TimeManager>().fixedDeltaTime;
        _rb2d.SetRotation(rotation);
        // transform.up = Vector3.ProjectOnPlane(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, transform.forward); ;
    }

    public void PickUpDirt()
    {
        Debug.Log("Pickup dirt");
    }
}

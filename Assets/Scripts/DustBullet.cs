using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class DustBullet : MonoBehaviour
{
    public ParticleSystem impactParticle;
    public float speed = 10;

    Rigidbody2D _rb2d;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb2d.MovePosition(transform.position + transform.up * speed * Director.GetManager<TimeManager>().fixedDeltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Instantiate(impactParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

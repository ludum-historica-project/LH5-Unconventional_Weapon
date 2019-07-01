using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CharacterController2D>())
        {
            collision.GetComponent<CharacterController2D>().PickUpDirt();
            Destroy(gameObject);
        }
    }
}

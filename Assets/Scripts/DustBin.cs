using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBin : MonoBehaviour
{

    DustDropper _activeDropper;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DustDropper>() != null)
        {
            _activeDropper = collision.GetComponent<DustDropper>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DustDropper>() == _activeDropper)
        {
            _activeDropper = null;
        }
    }

    private void Update()
    {
        if (_activeDropper != null)
        {
            _activeDropper.DropDust();
        }
    }
}

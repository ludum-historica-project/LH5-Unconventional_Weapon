using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMote : MonoBehaviour
{

    public int dustValue = 1;

    public DustPicker target;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Director.GetManager<TimeManager>().Paused)
            if (target)
            {
                transform.localScale *= .9f;
                transform.position = Vector3.Lerp(transform.position, target.transform.position, .33333f);
                if (transform.localScale.magnitude <= .05f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, .1f);

            }
    }

    public void PickUp(DustPicker picker)
    {
        if (target) return;
        target = picker;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}

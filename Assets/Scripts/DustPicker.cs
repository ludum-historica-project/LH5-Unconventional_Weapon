using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustPicker : MonoBehaviour
{
    public DustController dustController;
    public ScriptableEvent OnDustPickedUpEvent;

    public void PickUpDust(DustMote pocket)
    {
        OnDustPickedUpEvent.Raise();
        dustController.PickupDust(pocket.dustValue);
        pocket.PickUp(this);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<DustMote>())
        {
            PickUpDust(collision.collider.GetComponent<DustMote>());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DustMote>())
        {
            PickUpDust(collision.GetComponent<DustMote>());
        }
    }
}

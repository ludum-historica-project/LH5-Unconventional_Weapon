using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustShooter : MonoBehaviour
{
    public DustController dustController;

    public DustBullet bulletPrefab;

    public float shootCooldown;

    public float dustCost = 1;

    private float _currentShootCooldown;

    bool superMode;


    public void EnableSuperMode(bool enabled)
    {
        superMode = enabled;
    }

    Vector2 lastPositiveShootVector = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        Vector2 shootVector = new Vector2(Input.GetAxis("HorizontalFire"), Input.GetAxis("VerticalFire"));
        if (superMode && shootVector.magnitude <= Mathf.Epsilon)
        {
            shootVector = lastPositiveShootVector;
        }

        if (shootVector.magnitude > 0 && _currentShootCooldown >= (superMode ? .25f : 1) * shootCooldown && dustController.dustCount > 0)
        {
            lastPositiveShootVector = shootVector;
            ShootBullet(shootVector);
            _currentShootCooldown = 0;
            if (!superMode) dustController.RemoveDust(dustCost);
        }
        if (_currentShootCooldown < shootCooldown)
        {
            _currentShootCooldown += Director.GetManager<TimeManager>().deltaTime;
        }
    }

    void ShootBullet(Vector2 direction)
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity).transform.up = direction;
    }
}

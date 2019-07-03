using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]
public abstract class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float _currentHealth;
    protected EnemyMovement _movement;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _currentHealth = maxHealth;
        _movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

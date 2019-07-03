using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;

    public float currentHealth { get; private set; }

    public ScriptableEvent OnPlayerHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        OnPlayerHealthChanged.Raise();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SubscribeToDirector();
    }

    protected abstract void SubscribeToDirector();

}

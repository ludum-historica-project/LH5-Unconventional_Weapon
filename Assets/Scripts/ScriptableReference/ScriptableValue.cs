using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableValue<T> : ScriptableObject
{
    [SerializeField]
    protected T _value;
    public virtual T value
    {
        get { return _value; }
        set { _value = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public ScriptableEvent scriptableEvent;
    public UnityEvent response;
    private void OnEnable()
    {
        if (scriptableEvent) scriptableEvent.RegisterListener(this);
        else Destroy(this);
    }

    private void OnDisable()
    {
        if (scriptableEvent) scriptableEvent.UngeristerListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newScriptableEvent", menuName = "Scriptables/Events")]
public class ScriptableEvent : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!_listeners.Contains(listener)) _listeners.Add(listener);
    }

    public void UngeristerListener(GameEventListener listener)
    {
        if (_listeners.Contains(listener)) _listeners.Remove(listener);
    }
}

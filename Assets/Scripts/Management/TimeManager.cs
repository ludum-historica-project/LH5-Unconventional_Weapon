using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public ScriptableEvent OnPauseEvent;
    public ScriptableEvent OnUnpauseEvent;

    public System.Action<bool> OnPauseToggle = (p) => { };

    private bool _paused;
    public bool Paused
    {
        get
        {
            return _paused;
        }
        set
        {
            if (value != _paused)
            {
                if (value) OnPauseEvent.Raise();
                else OnUnpauseEvent.Raise();
                _paused = value;
                OnPauseToggle(value);
            }
        }
    }


    private void Start()
    {
        OnPauseToggle += TogglePhysics;
    }

    void TogglePhysics(bool paused)
    {
        Physics2D.autoSimulation = Physics.autoSimulation = !paused;
    }

    public float deltaTime
    {
        get
        {
            return Paused ? 0 : Time.deltaTime;
        }
    }

    public float fixedDeltaTime
    {
        get
        {
            return Paused ? 0 : Time.fixedDeltaTime;
        }
    }



}

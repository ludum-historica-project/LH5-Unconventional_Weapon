using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public int score { get; private set; }

    public ScriptableEvent ScoreChangedEvent;

    public void AddScore(int toAdd)
    {
        score += toAdd;
        ScoreChangedEvent.Raise();
    }
}

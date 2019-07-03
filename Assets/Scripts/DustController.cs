using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    public float dustCount;
    public int maxDust;

    public ScriptableEvent OnSupermodeEnabled;
    public ScriptableEvent OnSupermodeDisabled;

    public ScriptableEvent OnDustCountChanged;
    public DustPicker dustPicker;

    public float superModeDuration;
    bool superMode = false;

    public void PickupDust(int count)
    {
        float _prevDust = dustCount;
        if (!superMode) dustCount = Mathf.Clamp(dustCount + count, 0, maxDust);
        Director.GetManager<ScoreManager>().AddScore(count);
        if (dustCount != _prevDust) OnDustCountChanged.Raise();
    }

    public void RemoveDust(float dropped)
    {
        float _prevDust = dustCount;
        dustCount = Mathf.Clamp(dustCount - dropped, 0, maxDust);
        if (dustCount <= 0)
        {
            superMode = false;
            OnSupermodeDisabled.Raise();
        }
        if (dustCount != _prevDust) OnDustCountChanged.Raise();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && dustCount >= maxDust)
        {
            Debug.Log("Supermode Activated");
            superMode = true;
            OnSupermodeEnabled.Raise();
        }

        if (superMode)
        {
            RemoveDust(Director.GetManager<TimeManager>().deltaTime * maxDust / superModeDuration);
        }
    }

    // substract x/sec
    // last t seconds
    // 

    // dist = time * speed
    // dist = maxDust
    // time = supermodeDuration
    // speed = distance / time


}

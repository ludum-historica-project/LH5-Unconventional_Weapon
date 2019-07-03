using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDropper : MonoBehaviour
{
    public float dustDroppedPerSecond = 10;
    public DustController dustController;

    public void DropDust()
    {
        dustController.RemoveDust(dustDroppedPerSecond * Director.GetManager<TimeManager>().deltaTime);
    }

}

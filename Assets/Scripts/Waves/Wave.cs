using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newWave", menuName = "Scriptables/Wave/Wave")]

public class Wave : ScriptableObject
{
    [System.Serializable]
    public class WavePart
    {
        public WaveArea area;
        public int enemyCount;
        public bool instant; //if instant, all enemies appear at once
        public bool ordered; // if ordered, all enemies will appear following a clockwise order
        public bool edge; // will only appear on edge of area;
    }

    public List<WavePart> parts;
}

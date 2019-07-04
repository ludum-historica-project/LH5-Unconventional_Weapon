using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Scriptables/Values/SoundLibrary")]
public class SoundLibrary : SoundValue
{
    public List<AudioClip> clips = new List<AudioClip>();
    public override AudioClip value
    {
        get { return clips[Random.Range(0, clips.Count)]; }
    }
}

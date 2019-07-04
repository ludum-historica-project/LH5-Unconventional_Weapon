using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayOnAwake : MonoBehaviour
{
    public SoundValue music;
    // Start is called before the first frame update
    void Start()
    {
        Director.GetManager<SoundManager>().PlayMusic(music);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

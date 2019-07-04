using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : Manager
{
    public AudioMixer master;
    public AudioMixerGroup soundGroup;
    public AudioMixerGroup musicGroup;

    private List<AudioSource> _usedSources = new List<AudioSource>();
    private List<AudioSource> _freeSources = new List<AudioSource>();

    private AudioSource musicSource;

    public float masterVolume { get; private set; } = 1;
    public float musicVolume { get; private set; } = 1;
    public float soundsVolume { get; private set; } = 1;

    private void Start()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        soundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 1);

        SetMasterVolumeScalar(masterVolume);
        SetMusicVolumeScalar(musicVolume);
        SetSoundsVolumeScalar(soundsVolume);
    }



    public void PlayMusic(SoundValue music, bool forceRestart = false)
    {
        if (musicSource == null) //first song
        {
            musicSource = new GameObject().AddComponent<AudioSource>();
            musicSource.transform.parent = transform;
            musicSource.gameObject.name = "Music";
        }
        if (musicSource.clip != music.value || forceRestart)
        {
            AssignSoundToSource(musicSource, music);
            musicSource.Play();
        }
    }

    public void PlaySound(SoundValue sound)
    {
        AudioSource source;
        if (_freeSources.Count == 0)
        {
            source = new GameObject().AddComponent<AudioSource>();
            source.transform.parent = transform;
        }
        else
        {
            source = _freeSources[0];
            _freeSources.RemoveAt(0);
        }
        _usedSources.Add(source);
        source.gameObject.name = "Sound: " + sound.name;
        AssignSoundToSource(source, sound);
        source.Play();
    }

    float LinearToDB(float linear)
    {
        return Mathf.Log(linear, 5) * 20;
    }

    public void SetMasterVolumeScalar(float vol)
    {
        PlayerPrefs.SetFloat("MasterVolume", vol);
        master.SetFloat("MasterVolume", LinearToDB(vol));
        masterVolume = vol;
    }

    public void SetMusicVolumeScalar(float vol)
    {
        PlayerPrefs.SetFloat("MusicVolume", vol);
        master.SetFloat("MusicVolume", LinearToDB(vol));
        musicVolume = vol;
    }


    public void SetSoundsVolumeScalar(float vol)
    {
        PlayerPrefs.SetFloat("SoundsVolume", vol);
        master.SetFloat("SoundsVolume", LinearToDB(vol));
        soundsVolume = vol;
    }


    void AssignSoundToSource(AudioSource source, SoundValue sound)
    {
        source.clip = sound.value;
        source.volume = sound.volume;
        source.loop = sound.loop;
        source.pitch = sound.pitch;
        if (sound.mixerGroup == null) source.outputAudioMixerGroup = soundGroup;
        else source.outputAudioMixerGroup = sound.mixerGroup;
    }
    private void Update()
    {
        for (int i = _usedSources.Count - 1; i >= 0; i--)
        {
            if (!_usedSources[i].isPlaying)
            {
                _usedSources[i].gameObject.name = "[Free AudioSource]";
                _freeSources.Add(_usedSources[i]);
                _usedSources.RemoveAt(i);

            }
        }
    }

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
}
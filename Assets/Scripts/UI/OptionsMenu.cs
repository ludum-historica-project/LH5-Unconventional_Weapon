using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class OptionsMenu : UIWindow
{

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;


    private void OnEnable()
    {
        Director.GetManager<TimeManager>().Paused = true;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Director.GetManager<TimeManager>().Paused = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        masterVolumeSlider.value = Director.GetManager<SoundManager>().masterVolume;
        masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);

        musicVolumeSlider.value = Director.GetManager<SoundManager>().musicVolume;
        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);

        soundVolumeSlider.value = Director.GetManager<SoundManager>().soundsVolume;
        soundVolumeSlider.onValueChanged.AddListener(UpdateSoundsVolume);
    }

    void UpdateMasterVolume(float value)
    {
        Director.GetManager<SoundManager>().SetMasterVolumeScalar(value);
    }

    void UpdateMusicVolume(float value)
    {
        Director.GetManager<SoundManager>().SetMusicVolumeScalar(value);
    }

    void UpdateSoundsVolume(float value)
    {
        Director.GetManager<SoundManager>().SetSoundsVolumeScalar(value);
    }

    public void Return()
    {
        gameObject.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    protected override void OnInputDevicePress(bool isJoystick)
    {
        //throw new System.NotImplementedException();
    }
}

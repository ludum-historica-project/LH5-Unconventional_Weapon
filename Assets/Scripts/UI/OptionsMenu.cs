using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    public List<Selectable> selectables = new List<Selectable>();

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

    public System.Action OnClose = () => { };
    bool pressingCancelOnPreviousframe = true;
    private void OnEnable()
    {
        Director.GetManager<TimeManager>().Paused = true;
    }

    private void OnDisable()
    {
        Director.GetManager<TimeManager>().Paused = false;
        OnClose();
    }

    // Start is called before the first frame update
    void Start()
    {
        selectables = new List<Selectable>(GetComponentsInChildren<Selectable>());

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

    // Update is called once per frame
    private void Update()
    {
        RefocusForJoystick();
        if (!pressingCancelOnPreviousframe && Input.GetAxis("Cancel") > 0) Return();
        pressingCancelOnPreviousframe = Input.GetAxis("Cancel") > 0;
    }

    void RefocusForJoystick()
    {
        if (selectables.Count == 0) return;
        foreach (var selectable in selectables)
        {
            if (EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                return;
            }
        }
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                selectables[0].Select();
            }
        }
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
}

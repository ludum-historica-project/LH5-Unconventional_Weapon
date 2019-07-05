using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenu : UIWindow
{
    public List<Button> buttons = new List<Button>();
    public OptionsMenu options;
    public HowToPlayWindow howToPlay;
    bool focused = true;
    protected override void Update()
    {
        if (focused)
            RefocusForJoystick();
    }


    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Options()
    {
        focused = false;
        foreach (var button in buttons) button.interactable = false;
        options.gameObject.SetActive(true);
        options.OnClose += OnRefocus;
    }

    public void HowToPlay()
    {
        focused = false;
        foreach (var button in buttons) button.interactable = false;
        howToPlay.gameObject.SetActive(true);
        howToPlay.OnClose += OnRefocus;
    }

    void OnRefocus()
    {
        focused = true;
        options.OnClose -= OnRefocus;
        foreach (var button in buttons) button.interactable = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    protected override void OnInputDevicePress(bool isJoystick)
    {
        //throw new System.NotImplementedException();
    }
}

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
    bool focused = true;
    private void Update()
    {
        if (focused)
            RefocusForJoystick();
    }

    void RefocusForJoystick()
    {
        if (buttons.Count == 0) return;
        foreach (var button in buttons)
        {
            if (EventSystem.current.currentSelectedGameObject == button.gameObject)
            {
                return;
            }
        }
        if (Input.GetAxis("AnyJoystickAxis") > 0)
        {
            buttons[0].Select();
            return;
        }
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                buttons[0].Select();
            }
        }
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
}

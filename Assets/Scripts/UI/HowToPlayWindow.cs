using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayWindow : UIWindow
{
    public GameObject joystickView;
    public GameObject keyboardView;

    protected override void OnInputDevicePress(bool isJoystick)
    {
        joystickView.SetActive(isJoystick);
        keyboardView.SetActive(!isJoystick);
    }
}

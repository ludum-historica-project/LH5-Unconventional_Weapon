using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public abstract class UIWindow : MonoBehaviour
{
    public List<Selectable> selectables = new List<Selectable>();


    public System.Action OnClose = () => { };
    protected bool pressingCancelOnPreviousframe = true;

    protected virtual void Start()
    {
        selectables = new List<Selectable>(GetComponentsInChildren<Selectable>());

    }

    protected virtual void OnDisable()
    {
        OnClose();
    }

    protected virtual void Update()
    {
        RefocusForJoystick();
        if (!pressingCancelOnPreviousframe && Input.GetAxis("Cancel") > 0) gameObject.SetActive(false);
        pressingCancelOnPreviousframe = Input.GetAxis("Cancel") > 0;
        if (Input.anyKey) OnInputDevicePress(false);
        if (AnyJoystickInput()) OnInputDevicePress(true);
    }

    protected abstract void OnInputDevicePress(bool isJoystick);


    protected void RefocusForJoystick()
    {
        if (selectables.Count == 0) return;
        foreach (var selectable in selectables)
        {
            if (EventSystem.current.currentSelectedGameObject == selectable.gameObject)
            {
                return;
            }
        }
        if (AnyJoystickInput()) selectables[0].Select();
    }

    protected bool AnyJoystickInput()
    {

        if (Input.GetAxis("AnyJoystickAxis") != 0)
        {
            return true;
        }

        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKey("joystick 1 button " + i))
            {
                Debug.Log("joystick 1 button " + i + " is pressed");
                return true;
            }
        }
        return false;
    }
}

using System;
using InControl;
using UnityEngine;

public class InputMagic : MonoBehaviour
{
    //TODO put to static to see if wurk or nut
    // ================================================================================================ Public variables
    public Action<int, int, bool> _ActionKeys = delegate(int i, int i1, bool arg3) { };
    public Action<int, ControllerEnums.Type, bool> _BackButtons = delegate(int i, ControllerEnums.Type type, bool arg3) {  };
    public Action<int, ControllerEnums.Type, float> _BackTrigger = delegate(int i, ControllerEnums.Type type, float arg3) {  };
    public Action<int, ControllerEnums.Type, float, float> _Analog = delegate(int i, ControllerEnums.Type type, float arg3, float arg4) {  };
    public Action<int, ControllerEnums.Type, float> _Dpad = delegate(int i, ControllerEnums.Type type, float arg3) {  };
    public Action<int, ControllerEnums.Type, bool> _Options = delegate(int i, ControllerEnums.Type type, bool arg3) {  };

    // =============================================================================================== Private variables
    private ControllerHolder _holder;
    private int _amount;

    // =========================================================================================================== Awake
    private void Awake()
    {
        _holder = GameObject.Find("@ControllerHolder").GetComponent<ControllerHolder>();
        _amount = _holder._players.Count;
    }

    // ========================================================================================================== Update
    private void Update()
    {
        InputDevice pl;
        for (int i = 0; i < _amount; i++)
        {
            pl = _holder._players[i];
            CheckActionInput(pl, i);
            CheckBackButtons(pl, i);
            CheckAnalog_Dpad(pl, i);
            CheckOptions(pl, i);
        }

    }

    // ================================================================================================ CheckActionInput
    private void CheckActionInput(InputDevice pl, int i)
    {
        _ActionKeys(i,1,pl.Action1.WasPressed);
        _ActionKeys(i,2,pl.Action2.WasPressed);
        _ActionKeys(i,3,pl.Action3.WasPressed);
        _ActionKeys(i,4,pl.Action4.WasPressed);
    }

    // ================================================================================================ CheckBackButtons
    private void CheckBackButtons(InputDevice pl, int i)
    {
        _BackButtons(i, ControllerEnums.Type.BackRight, pl.RightBumper);
        _BackButtons(i, ControllerEnums.Type.BackLeft, pl.LeftBumper);
        _BackTrigger(i, ControllerEnums.Type.BackTriggerRight, pl.RightTrigger.Value);
        _BackTrigger(i, ControllerEnums.Type.BackTriggerLeft, pl.LeftTrigger.Value);
    }

    // ================================================================================================ CheckAnalog_Dpad
    private void CheckAnalog_Dpad(InputDevice pl, int i)
    {
        _Analog(i, ControllerEnums.Type.RightAnalogStick, pl.RightStick.X, pl.RightStick.Y);
        _Analog(i, ControllerEnums.Type.LeftAnalogStick, pl.LeftStick.X, pl.LeftStick.Y);
        _Dpad(i, ControllerEnums.Type.DpadX, pl.DPad.X);
        _Dpad(i, ControllerEnums.Type.DpadY, pl.DPad.Y);
    }

    private void CheckOptions(InputDevice pl, int i)
    {
        _Options(i, ControllerEnums.Type.Command, pl.Command.State);
    }
}

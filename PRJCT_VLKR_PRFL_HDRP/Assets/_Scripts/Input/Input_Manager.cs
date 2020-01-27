using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{

    private bool canInput = false;

    public void SetAllowInput(bool i)
    {
        canInput = i;
    }

    public bool GetButton(Enum_Buttons.Buttons button, Enum_Buttons.ButtonState buttonState, int player)
    {
        if (canInput)
        {
            switch (buttonState)
            {
                case Enum_Buttons.ButtonState.GoingDown:
                    return Input.GetButtonDown(button.ToString() + player);
                case Enum_Buttons.ButtonState.IsDown:
                    return Input.GetButton(button.ToString() + player);
                case Enum_Buttons.ButtonState.GoingUp:
                    return Input.GetButtonUp(button.ToString() + player);
            }
        }
        return false;
    }

    public float GetTrigger(Enum_Buttons.Axis axis, int player)
    {
        return Input.GetAxisRaw(axis.ToString() + player);
    }

    void Update()
    {
        //WHERETHEFUCKAREMYCONTROLLERS();
    }


    private void WHERETHEFUCKAREMYCONTROLLERS()
    {
        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("Cross" + i))
            {
                print("Cross" + i);
            }
        }
    }
}

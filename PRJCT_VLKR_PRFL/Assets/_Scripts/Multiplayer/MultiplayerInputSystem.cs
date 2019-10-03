using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MultiplayerInputSystem : MonoBehaviour
{

    private bool _canLookForInput = true;
    public void SetAllowInput(bool i)
    {
        _canLookForInput = i;
    }

    MultiplayerControllerHolder _mpCH;
    InputHandler _input;


    void Start()
    {
        _mpCH = GameObject.Find("MultiplayerControllerHolder").GetComponent<MultiplayerControllerHolder>();
        _input = GameObject.Find("InputManager").GetComponent<InputHandler>();
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (_canLookForInput) { UpdateInput(i); }
        }
    }

    void UpdateInput(int player)
    {
        // easy reference
        InputDevice cP = _mpCH._players[player];
        
        // actions handling
        bool[] actions = { cP.Action1.WasPressed, cP.Action2.WasPressed, cP.Action3.WasPressed, cP.Action4.WasPressed };
        _input.GetActionInput(player, actions);

        // Rtrigger handling 
        _input.GetTriggerInput(player, cP.RightTrigger);
    }
}

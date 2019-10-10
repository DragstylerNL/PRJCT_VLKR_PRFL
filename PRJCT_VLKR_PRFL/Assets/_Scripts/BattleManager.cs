using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private bool _ActiveAttackSystem { get; } = false;
    private ActionSceneSwitch _actionSceneSwitch;
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<Input_Manager>().SetAllowInput(true);
        _actionSceneSwitch = GetComponent<ActionSceneSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AT) { TimeRunning += Time.deltaTime; }
        if(TimeRunning > WaitTimer) { StopActionsTime(); }
    }

    /// <summary>
    /// for instantiating and continuing the active BattleSystem
    /// </summary>
    public void ActionTaken(int button)
    {
        if (AT)
        {
            WaitTimer = TimeRunning + 1;
            _actionSceneSwitch.SetButtonsPos(button);
        }
        else
        {
            ActionTime(button);
        }
    }

    bool AT = false;
    float WaitTimer = 1f;
    float TimeRunning = 0f;
    void ActionTime(int button)
    {
        AT = true;
        _actionSceneSwitch.StartAppearing();
        _actionSceneSwitch.SetButtonsPos(button);
    }
    void StopActionsTime()
    {
        _actionSceneSwitch.StartDisAppearing();

        AT = false;
        WaitTimer = 1;
        TimeRunning = 0;
    }
}

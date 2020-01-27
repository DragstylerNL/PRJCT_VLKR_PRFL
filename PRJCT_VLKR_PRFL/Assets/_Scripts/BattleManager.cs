using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private bool _ActiveAttackSystem { get; } = false;
    private ActionSceneSwitch _actionSceneSwitch;
    
    void Start()
    {
        // at the start of the battle the AllowInput should be changed to true
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<Input_Manager>().SetAllowInput(true);
        // get the ActionSceneSwitcher and add the refrence to this script
        _actionSceneSwitch = GetComponent<ActionSceneSwitch>();
    }


    void Update()
    {
        // if "runing" => add time.deltaTime to the timer
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


    // Custom Ienumerator for keeping in check wheter or not the ActionScene should show
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

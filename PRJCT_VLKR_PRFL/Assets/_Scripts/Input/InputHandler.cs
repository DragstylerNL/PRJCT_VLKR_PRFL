using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    TeamScript[] teams = new TeamScript[2];

    void Start()
    {
        // get the team's their script for sending input
        teams[0] = GameObject.Find("Player1").GetComponent<TeamScript>();
        teams[1] = GameObject.Find("Player2").GetComponent<TeamScript>();
    }

    /// <summary>
    /// Function that is used by the inputsystem to send information about the action states to this class that then will be send to the corrosponding team
    /// </summary>
    /// <param name="player">what player in the array order are we talking about</param>
    /// <param name="action">a boolean array containing action 1 through 4</param>
    public void GetActionInput(int player, bool[] action)
    {
        for (int i = 0; i < action.Length; i++)
        {
            if (action[i])
            {
                teams[player].ActionGotTriggered(i + 1);
                GameObject.Find("BattleManager").GetComponent<BattleManager>().ActionTaken(i+1);
            }
            
        }
    }

    /// <summary>
    /// Function that is used by the inputsystem to send information about the trigger state to this class that then will be send to the corrosponding team
    /// </summary>
    /// <param name="player">what player in the array order are we talking about</param>
    /// <param name="triggerState">the state of the trigger being either true or false</param>
    public void GetTriggerInput(int player, bool triggerState)
    {
        teams[player].TriggerGotTriggered(triggerState);
    }
}

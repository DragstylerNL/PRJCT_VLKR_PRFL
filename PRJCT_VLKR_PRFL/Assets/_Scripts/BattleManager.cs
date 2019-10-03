using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private bool _ActiveAttackSystem { get; } = false;
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<Input_Manager>().SetAllowInput(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// for instantiating and continuing the active BattleSystem
    /// </summary>
    public void ActionTaken()
    {

    }
}

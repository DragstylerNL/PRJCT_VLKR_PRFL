using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().SetAllowInput(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

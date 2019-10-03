using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class MultiplayerControllerHolder : MonoBehaviour
{

    public List<InputDevice> _players = new List<InputDevice>();

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}

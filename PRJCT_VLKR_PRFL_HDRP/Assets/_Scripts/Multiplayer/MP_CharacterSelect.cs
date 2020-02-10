using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_CharacterSelect : MonoBehaviour
{
    public GameObject selectorScreen;

    bool _startLerp = false;

    public void StartSelection()
    {
        _startLerp = true;
    }

    void Update()
    {
        if (_startLerp) { }
    }

}

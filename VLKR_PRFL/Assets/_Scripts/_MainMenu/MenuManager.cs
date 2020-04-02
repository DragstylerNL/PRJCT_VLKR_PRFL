using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, menuDisable;
    [SerializeField] private GameObject MP_BattleSelector, BattleDisable;

    private MenuManager _mm;

    private void Awake()
    {
        
    }

    public void StartMultiplayer()
    {
        menuDisable.SetActive(false);
        mainMenu.GetComponent<LerpTo>().StartLerp();
        BattleDisable.SetActive(true);
        MP_BattleSelector.GetComponent<LerpTo>().StartLerp();
    }
}

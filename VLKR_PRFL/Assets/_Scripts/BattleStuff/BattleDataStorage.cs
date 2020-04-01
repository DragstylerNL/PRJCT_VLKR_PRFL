using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataStorage : MonoBehaviour
{
    public List<CharacterHolder>[] playerCharacters = new List<CharacterHolder>[2];
    public void SetCharacters(int i, CharacterHolder[] chars)
    {
        foreach (CharacterHolder ch in chars)
        {
            playerCharacters[i].Add(ch);
        }
    }
    
    
}

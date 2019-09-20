using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/New Character", order = 1)]
public class CharacterHolder : ScriptableObject
{

    public string _cName;
    public int _cHealth;
    public int _cActionPoints;
    public int _cAttackDamage;
    public int _cAttackApNeed;
    
    public GameObject _cModelPrefab;

}

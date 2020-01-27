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
    public int _cDefenceApNeed;
    
    public GameObject _cModelPrefab;
    public GameObject _cShieldModel;

    public Sprite _cIdleState;
    //public Sprite _cPreparingState;
    public Sprite _cAttackingState;
    //public Sprite _cBlockingState;

}

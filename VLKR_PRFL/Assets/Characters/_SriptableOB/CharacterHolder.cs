using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Characters/New Character", order = 2)]
public class CharacterHolder : ScriptableObject
{
    [Header("Basic unit stats")]
    public string _cName;
    public int _cHealth;
    public int _cActionPoints;
    public int _cAttackDamage;
    public int _cAttackApNeed;
    public int _cDefenceApNeed;
    
    [Header("unit's object containing anim")]
    public GameObject _cModelPrefab;

    [Header("unit's profile picture")]
    public Sprite _cProfilePic;
}

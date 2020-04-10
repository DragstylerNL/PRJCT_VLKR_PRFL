using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCharacter : MonoBehaviour
{
    // =============================================================================================== private variables
    private GameObject _cActiveGameObject;
    private Animator anim;
    private float _dashSpeed = 0.05f;
    private Vector3 _basePos;
    private int _playerNumber;
    
    // ============================================================================================= 'private' variables
    [SerializeField] private SpriteRenderer _buttonUI;
    [SerializeField] private Slider _healthSlider;

    // ================================================================================================ public variables
    public string _cName;
    public float _cHealthMax;
    public float _cHealthCurrent;
    public int _cStaminaMax;
    public int _cStaminaCurrent;
    public float _cStaminaRegen;
    public float _cDamage;
    public CharacterState.Type _cState = CharacterState.Type.Idle;

    
    
    // =========================================================================================================== Start
    private void Start()
    {
        _cActiveGameObject = this.gameObject;
        _basePos = transform.position;
    }

    // ======================================================================================================= Set Stats
    public void SetStats(string name, float maxHealth, int maxStamina, float staminaRegen, float damage, int player, Sprite buttonUi)
    {
        _cName = name;
        _cHealthCurrent = _cHealthMax = maxHealth;
        _cStaminaCurrent = _cStaminaMax = maxStamina;
        _cStaminaRegen = staminaRegen;
        _cDamage = damage;
        _playerNumber = player;
        _buttonUI.sprite = buttonUi;
        UpdateHealthSlider();
    }

    private void Update()
    {
        
    }
    
    // ========================================================================================================== Attack
    public void Attack(ActiveCharacter enemy)
    {
        print("made it");
        if(_cState != CharacterState.Type.Idle &&
           _cState != CharacterState.Type.IdleDamaged &&
           _cState != CharacterState.Type.Block) return;
        _cState = CharacterState.Type.Dash;
        print("cleared the passage");
        StartCoroutine(IDash(enemy, true));
    }

    private void Dash(ActiveCharacter enemy)
    {
        //TODO ANIMATION dash anim 
        //
        
        
    }

    IEnumerator IDash(ActiveCharacter enemy, bool dashToEnemy)
    {
        float offset = _playerNumber == 0 ? -2 : 2;
        Vector3 targetPos = dashToEnemy == true ? enemy.transform.position : _basePos;
        targetPos.x += dashToEnemy ? offset : 0;
        while (transform.position != targetPos)
        {
            Vector3 currentFrameTarget = Vector3.zero;
            Vector3 currenPos = transform.position;
            currentFrameTarget.x = Mathf.Lerp(currenPos.x, targetPos.x, _dashSpeed);
            if(currentFrameTarget.x >= targetPos.x -0.1f && currentFrameTarget.x <= targetPos.x +0.1f){currentFrameTarget.x = targetPos.x;}
            currentFrameTarget.z = Mathf.Lerp(currenPos.z, targetPos.z, _dashSpeed);
            if(currentFrameTarget.z >= targetPos.z -0.1f && currentFrameTarget.z <= targetPos.z +0.1f){currentFrameTarget.z = targetPos.z;}
            transform.position = new Vector3(currentFrameTarget.x, currenPos.y, currentFrameTarget.z);
            yield return new WaitForEndOfFrame();
        }
        if(dashToEnemy){StartCoroutine(IAttack(enemy));}
        if(!dashToEnemy){_cState = CharacterState.Type.Idle;}
        yield return new WaitForEndOfFrame();
    }

    IEnumerator IAttack(ActiveCharacter enemy)
    {
        print(enemy._cHealthCurrent);
        enemy.GetAttacked(_cDamage);
        print(enemy._cHealthCurrent);
        StartCoroutine(IDash(enemy, false));
        yield return 0;
    }


    private void GetAttacked(float cDamage)
    {
        _cHealthCurrent -= cDamage;
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        _healthSlider.value = _cHealthCurrent / _cHealthMax;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCharacter : MonoBehaviour
{
    // needed references
    private CharacterHolder _character;
    private InputManager _InputManager;
    private Enum_Buttons.Buttons _button;
    private TeamScript enemyScript;

    // player world stuff
    private Vector3 _desiredPosition;
    private GameObject _graphics;
    private int _characterInOrder;
    private int _playerNumber;

    // player ActionPoints 
    private int _maxActionPoints, _currentActionPoints;
    private Slider _slider;

    // general player stats
    private bool _canDoSomething = true;
    public bool _isBlocking { get; private set; } = false;

    /// <summary>
    /// intantiating of this player
    /// </summary>
    /// <param name="character">character stats and its scriptableobj</param>
    /// <param name="desPos">the pos this character needs to be in</param>
    /// <param name="i"> player number in the position calc</param>
    /// <param name="playernumber"> player 1 or player 2</param>
    public void SummonToTheField(CharacterHolder character, Vector3 desPos, int i, int playernumber)
    {
        _character = character;
        transform.position = _desiredPosition = desPos;
        _characterInOrder = i + 1;
        _playerNumber = playernumber;

        _graphics = Instantiate(_character._cModelPrefab);
        _graphics.transform.parent = gameObject.transform;
        _graphics.transform.localPosition = Vector3.zero;
        //_graphics.transform.position = desPos;

        _currentActionPoints = _maxActionPoints = _character._cActionPoints;

        GetSlider();
    }

    /// <summary>
    /// get the slider that are linked to this character
    /// </summary>
    private void GetSlider()
    {
        _button = Enum_Buttons.Buttons.Circle;
        if(_characterInOrder == 1 && _playerNumber == 1) { _button = Enum_Buttons.Buttons.Circle; }
        else if(_characterInOrder == 1 && _playerNumber == 2) { _button = Enum_Buttons.Buttons.Square; }
        else if(_characterInOrder == 2) { _button = Enum_Buttons.Buttons.Cross; }
        else if (_characterInOrder == 3) { _button = Enum_Buttons.Buttons.Triangle; }
        else if (_characterInOrder == 4 && _playerNumber == 1) { _button = Enum_Buttons.Buttons.Square; }
        else if (_characterInOrder == 4 && _playerNumber == 2) { _button = Enum_Buttons.Buttons.Circle; }

        _slider = GameObject.FindGameObjectWithTag("AP" + _playerNumber).transform.Find(_button.ToString()).GetComponentInChildren<Slider>();
        _slider.value = _slider.maxValue = _maxActionPoints;
    }

    private void Start()
    {
        _InputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        int temp = _playerNumber==1? 2 : 1 ;
        enemyScript = GameObject.FindGameObjectWithTag("Player" + temp).GetComponent<TeamScript>();
    }

    private void Update()
    {
        if (_canDoSomething)
        {
            if (_InputManager.GetButton(_button, Enum_Buttons.ButtonState.GoingUp, _playerNumber) && _InputManager.GetTrigger(Enum_Buttons.Axis.Right_Trigger, _playerNumber) >= 0.1f)
            {
                if (_currentActionPoints >= _character._cDefenceApNeed)
                {
                    _currentActionPoints = 0;
                    _slider.value = 0;
                    _canDoSomething = false;
                    StartCoroutine(DefenceWait());
                }
            }
            else if (_InputManager.GetButton(_button, Enum_Buttons.ButtonState.GoingUp, _playerNumber))
            {
                if (_currentActionPoints >= _character._cAttackApNeed)
                {
                    _currentActionPoints -= _character._cAttackApNeed;
                    _slider.value = _currentActionPoints;
                    _canDoSomething = false;
                    StartCoroutine(AttackWait());
                }
            }
        }

        RegenerateEnergy();
    }

    IEnumerator AttackWait()
    {
        // position logic
        int temp = _playerNumber == 1 ? 1 : -1;
        // attack patterns
        bool isBlocked = enemyScript.TakeDamage(_character._cAttackDamage, _characterInOrder);
        if (isBlocked) { temp *= -1; }
        transform.position += new Vector3(1.5f * temp, 0, 0);

        // yield return
        yield return new WaitForSeconds(1f);

        // reset
        transform.position -= new Vector3(1.5f * temp, 0, 0);
        _canDoSomething = true;
    }

    IEnumerator DefenceWait()
    {
        // position logic
        int temp = _playerNumber == 1 ? 1 : -1;
        // shield prefab
        GameObject shield = Instantiate(_character._cShieldModel);
        shield.transform.parent = null;
        shield.transform.position = transform.position + new Vector3(0.5f * temp, 0, 0);
        // set block state
        _isBlocking = true;

        // yield return
        yield return new WaitForSeconds(2f);

        // reset
        Destroy(shield);
        _isBlocking = false;
        _canDoSomething = true;
    }

    private float secondTime = 0f;
    private void RegenerateEnergy()
    {
        if (_currentActionPoints != _maxActionPoints)
        {
            secondTime += Time.deltaTime;
            if(secondTime >= 1)
            {
                _currentActionPoints += 1;
                UpdateSlider();
                secondTime = 0;
            }
        }
    }

    private void UpdateSlider()
    {
        _slider.value = _currentActionPoints;
    }

}

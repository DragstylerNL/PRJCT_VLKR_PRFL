using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour
{
    public GameObject basePrefab;

    private int _playerNumber;
    public CharacterHolder[] _Characters;

    private int _pHealthPoints;
    private Slider _pHpSlider;

    private Dictionary<int, PlayingCharacter> _playerList = new Dictionary<int, PlayingCharacter>();
    private PlayingCharacter[] _ActiveCharacters = new PlayingCharacter[4];

    private bool _blocking = false;

    public int plnmbr;
    void Start()
    {
        SetPlayerStats(plnmbr, _Characters);
    }

    public void SetPlayerStats(int playerNumber, CharacterHolder[] characters)
    {
        _playerNumber = playerNumber;
        _Characters = characters;

        _pHpSlider = GameObject.FindGameObjectWithTag("HPbar" + _playerNumber).GetComponent<Slider>();
        _pHealthPoints = 0;
        for(int i = 0; i < 4; i++) { _pHealthPoints += _Characters[i]._cHealth; }
        _pHpSlider.value = _pHpSlider.maxValue = _pHealthPoints;

        Initialization();
    }

    private void Initialization()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject currentEdit = Instantiate(new GameObject());
            currentEdit.AddComponent<PlayingCharacter>();
            currentEdit.transform.parent = GameObject.Find("Player" + _playerNumber).transform;
            _ActiveCharacters[i] = currentEdit.GetComponent<PlayingCharacter>();
            _ActiveCharacters[i].SummonToTheField(_Characters[i], DesiredPos(i), i, _playerNumber);

            SetIntoDictionary(_ActiveCharacters[i], i);


        }
    }

    private Vector3 DesiredPos(int i)
    {
        int j = 1; if (_playerNumber == 1) { j = -1; }
        Vector3 pos = new Vector3();
        switch (i)
        {
            case 0:
                pos = new Vector3(2 * j, 1, 0);
                break;
            case 1: pos = new Vector3(3 * j, 1, -1);
                break;
            case 2: pos = new Vector3(3 * j, 1, 1);
                break;
            case 3: pos = new Vector3(4 * j, 1, 0);
                break;
        }
        return pos;
    }

    void SetIntoDictionary( PlayingCharacter charachter, int actionToLink)
    {
        int j = 1; if (_playerNumber == 1) { j = -1; }
        if(actionToLink == 0) {
            if (j == -1) { _playerList.Add(2, charachter); }
            else _playerList.Add(3, charachter);
        }
        else if(actionToLink == 1)
        {
            _playerList.Add(1, charachter);
        }
        else if (actionToLink == 2)
        {
            _playerList.Add(4, charachter);
        }
        if (actionToLink == 3)
        {
            if (j == -1) { _playerList.Add(3, charachter); }
            else _playerList.Add(2, charachter);
        }
        GameObject.Find("BattleManager").GetComponent<ActionSceneSwitch>()._playerList = _playerList;
    }

    public bool TakeDamage(int damage, int charachterUnderAttack)
    {
        if (!_ActiveCharacters[charachterUnderAttack - 1]._isBlocking)
        {
            _pHealthPoints -= damage;
            if (_pHealthPoints <= 0)
            {
                _pHealthPoints = 0;
                GameObject.FindGameObjectWithTag("InputManager").GetComponent<Input_Manager>().SetAllowInput(false);
            }
            UpdateHealthBar();
            return false;
        }
        return true;
    }

    private void UpdateHealthBar()
    {
        _pHpSlider.value = _pHealthPoints;
    }

    public void ActionGotTriggered(int action)
    {
        _playerList.TryGetValue(action, out PlayingCharacter character);
        if (!_blocking)
        {
            character.Attack();
        }
        else
        {
            character.Defend();
        }
    }

    public void TriggerGotTriggered(bool triggerState)
    {
        _blocking = triggerState;
    }
}

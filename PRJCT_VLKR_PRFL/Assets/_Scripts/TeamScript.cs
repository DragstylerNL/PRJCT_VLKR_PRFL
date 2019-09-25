using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScript : MonoBehaviour
{
    public GameObject basePrefab;

    private int _playerNumber;
    private CharacterHolder[] _Characters = new CharacterHolder[4];
    private PlayingCharacter[] _ActiveCharacters = new PlayingCharacter[4];

    private int _pHealthPoints;
    private Slider _pHpSlider;

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
            GameObject currentEdit = Instantiate(basePrefab);
            currentEdit.transform.parent = GameObject.Find("Player" + _playerNumber).transform;
            currentEdit.GetComponent<PlayingCharacter>().SummonToTheField(_Characters[i], DesiredPos(i), i, _playerNumber);
            _ActiveCharacters[i] = currentEdit.GetComponent<PlayingCharacter>();
        }
    }

    private Vector3 DesiredPos(int i)
    {
        int j = 1; if(_playerNumber == 1) { j = -1; }
        Vector3 pos = new Vector3();
        switch (i)
        {
            case 0: pos = new Vector3(2 * j, 1, 0);
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

    public bool TakeDamage(int damage, int charachterUnderAttack)
    {
        if (!_ActiveCharacters[charachterUnderAttack - 1]._isBlocking)
        {
            _pHealthPoints -= damage;
            if(_pHealthPoints <= 0)
            {
                _pHealthPoints = 0;
                GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().SetAllowInput(false);
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

    public int plnmbr;
    public CharacterHolder ch;
    void Start()
    {
        CharacterHolder[] holder = { ch, ch, ch, ch};
        SetPlayerStats(plnmbr, holder);
    }
}

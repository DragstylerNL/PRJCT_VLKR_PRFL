using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MP_CharacterSelect : MonoBehaviour
{
    // ============================================================================================== "public" variables
    [SerializeField] private Transform selectorScreen;
    [SerializeField] private GameObject selectables;
    [SerializeField] private GameObject selectablePrefab;
    
    [SerializeField] private CharacterCollection collection;
    [SerializeField] private ControllerHolder controllerHolder;
    [SerializeField] private MultiplayerCharacterHolder mpCharacterHolder;
    
    // =============================================================================================== private variables
    private float _selectableSpacing = 14;
    private Vector2[] _selectablesPositions;

    private Transform[] _playerSelectorPositions = new Transform[2];
    private int[] _playerIndexSelected = new int[2];
    private int[] _playerAmountSelected = new int[4];

    private bool _update;
    private bool _checkInput;
    
    // =========================================================================================================== Awake
    private void Awake()
    {
        int l = collection.characters.Length;
        
        _selectablesPositions = new Vector2[l];
        _playerIndexSelected[1] = l - 1;
        float half = _selectableSpacing / 2;
        float startingPos = -((half * l) - half);

        for (int i = 0; i < l; i++)
        {
            GameObject prefab =Instantiate(selectablePrefab, selectables.transform);
            prefab.GetComponent<Image>().sprite = collection.characters[i]._cProfilePic;
            prefab.transform.localPosition = _selectablesPositions[i] = new Vector2(startingPos + (_selectableSpacing * i),0);
        }

        for (int i = 0; i < 2; i++)
        {
            _playerSelectorPositions[i] = selectables.GetComponent<GameObjectHolder>().objects[i].transform;
            SetSelectorPos(i);
        }
    }

    // ====================================================================================== Start selecting Characters
    public void StartSelection()
    {
        StartCoroutine(ShowUi());
    }

    // ============================================================================================ Start showing the UI
    private IEnumerator ShowUi()
    {
        float destination = 0f;
        bool pos = true;
        while (pos)
        {
            selectorScreen.localPosition = new Vector3(0, Mathf.Lerp(selectorScreen.localPosition.y, destination, 0.05f));
            if (selectorScreen.localPosition.y < destination + 0.01f)
            {
                selectorScreen.localPosition = new Vector3(0, destination);
                pos = false;
                _update = _checkInput = true;
            }
            yield return 0;
        }
        yield return 0;
    }
    
    // ========================================================================================================== Update
    private void Update()
    {
        if (!_update) return;
        if (_checkInput)
        {
            for (int i = 0; i < 2; i++)
            {
                if (_playerAmountSelected[i] != 4) continue;
                if (controllerHolder._players[i].DPadLeft.WasPressed) 
                    MoveSelected(false, i);
                if (controllerHolder._players[i].DPadRight.WasPressed) 
                    MoveSelected(true, i);
                if (controllerHolder._players[i].Action1.WasPressed) 
                    Select(i);
            }
        }
    }

    // ========================================================================================================= On Move
    private void MoveSelected(bool toTheRight, int playerNumber)
    {
        int queuedNumber = toTheRight ? _playerIndexSelected[playerNumber] + 1 : _playerIndexSelected[playerNumber] - 1;
        if (queuedNumber <= collection.characters.Length - 1 && queuedNumber >= 0)
        {
            _playerIndexSelected[playerNumber] = queuedNumber;
            SetSelectorPos(playerNumber);
        }
    }
        
    // ============================================================================================ On Move set Selector
    private void SetSelectorPos(int i)
    {
        _playerSelectorPositions[i].localPosition = _selectablesPositions[_playerIndexSelected[i]];
    }
    
    // ======================================================================================================= On Select
    private void Select(int playerNumber)
    {
        mpCharacterHolder.AddCharacter(playerNumber, _playerAmountSelected[playerNumber], collection.characters[_playerIndexSelected[playerNumber]]);
    }
}

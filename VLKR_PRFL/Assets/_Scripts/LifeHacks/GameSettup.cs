using UnityEngine;

public class GameSettup : MonoBehaviour
{
    // ============================================================================================= 'Private' Variables
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private Sprite[] _buttonUi;
    
    // =============================================================================================== Private Variables
    private MultiplayerCharacterHolder _holder;
    private ActiveCharacter[,] _charactersOnTheField = new ActiveCharacter[2,4];
    private Vector3[,] _positioning = new Vector3[2,4];

    // =========================================================================================================== Start
    private void Start()
    {
        _holder = GameObject.Find("@PvP_Charachters").GetComponent<MultiplayerCharacterHolder>();
        
        // positioning 
        SetCharPosArray();
        for (int i= 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject currentEdit = Instantiate(_holder._selectedCharacters[i, j]._cModelPrefab);
                currentEdit.transform.position = _positioning[i, j];
                SpriteRenderer renderer = currentEdit.GetComponent<SpriteRenderer>();
                if (i == 1) renderer.flipX = true;
                renderer.sortingOrder = -j + 10;
                if (j == 1) renderer.sortingOrder += 5;
                
                CharacterHolder currentEditHolder = _holder._selectedCharacters[i, j];
                ActiveCharacter characterScript = currentEdit.GetComponent<ActiveCharacter>();
                characterScript.SetStats(
                    currentEditHolder._cName, 
                    (float)currentEditHolder._cHealth, 
                    currentEditHolder._cActionPoints,
                    currentEditHolder._cApRegen,
                    currentEditHolder._cAttackDamage,
                    i,
                    ButtonSprite(i,j)
                    );
                _charactersOnTheField[i, j] = characterScript;
            }
        }
        
        // call Battlemanager
        _battleManager.SetActiveChars(_charactersOnTheField);
        _battleManager.SetActive(true);
    }

    // ============================================================================ Set Character positions in the array
    private void SetCharPosArray()
    {
        _positioning[0,0] = new Vector3(-2.5f, 1.26f, 0);
        _positioning[0,1] = new Vector3(-4.25f, 1.26f, -2f);
        _positioning[0,2] = new Vector3(-4.25f, 1.26f, 2f);
        _positioning[0,3] = new Vector3(-6f, 1.26f, 0);

        for (int i = 0; i < 4; i++)
        {
            _positioning[1, i] = _positioning[0, i];
            _positioning[1, i].x *= -1f;
        }
    }

    private Sprite ButtonSprite(int player, int character)
    {
        switch (character)
        {
            case 0: return player == 0 ? _buttonUi[1] : _buttonUi[2];
                break;
            case 1: return _buttonUi[0];
                break;
            case 2: return _buttonUi[3];
                break;
            case 3: return player == 0 ? _buttonUi[2] : _buttonUi[1];
                break;
        }
        return null;
    }
}

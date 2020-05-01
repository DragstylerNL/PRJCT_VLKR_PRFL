using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // ============================================================================================= 'Private' Variables
    [SerializeField] private InputMagic _inputMagic;
    [SerializeField] private InBattleSelectorMP _selector;
    
    // =============================================================================================== Private Variables
    private ActiveCharacter[,] _charactersOnTheField = new ActiveCharacter[2,4];
    private int[] _playersToAttack = new int[2];
    private int[,] _actionToPlayer = new int[2,4];
    private bool[] _triggerState = new bool[2];

    private bool _update;
    
    // =========================================================================================== Set Active Characters
    public void SetActiveChars(ActiveCharacter[,] characters)
    {
        _charactersOnTheField = characters;
    }
    
    // ====================================================================================================== Set Active 
    public void SetActive(bool activate)
    {
        _update = activate;
    }

    // ================================================================================================================= Start
    private void Start()
    {
        _inputMagic._ActionKeys += ActionKeyUpdate;
        _inputMagic._Dpad += DpadUpdate;
        _inputMagic._BackTrigger += BackTriggersUpdate;

        SetActionToPlayer();
    }

    // ============================================================================================ Set Action To Player
    private void SetActionToPlayer()
    {
        for (int i = 0; i < 2; i++)
        {
            _actionToPlayer[i, 0] = 1;
            _actionToPlayer[i, 1] = i == 0 ? 0 : 3;
            _actionToPlayer[i, 2] = i == 0 ? 3 : 0;
            _actionToPlayer[i, 3] = 2;
        }
    }

    // ================================================================================================================= Update
    private void Update()
    {
        if(!_update) return;
    }
    
    // ============================================================================================== Action keys Update
    private void ActionKeyUpdate(int player, int actionNumber, bool actionState)
    {
        if(actionState == false) return;
        
        int other = player == 0 ? 1 : 0;

        if (_triggerState[player] == true) // BLOCK
        {
            _charactersOnTheField[player, _actionToPlayer[player, actionNumber-1]].Block();
        }
        else // ATTACK
        {
            _charactersOnTheField[player, _actionToPlayer[player, actionNumber-1]]
                .Attack(_charactersOnTheField[other, _playersToAttack[player]]);
        }
    }

    // ============================================================================================ Back Triggers Update
    private void BackTriggersUpdate(int player, ControllerEnums.Type leftOrRight, float value)
    {
        if (leftOrRight != ControllerEnums.Type.BackTriggerRight) return;

        _triggerState[player] = value >= 0.5f ? true : false;
    }
    
    // ==================================================================================================== D-pad Update
    private void DpadUpdate(int player, ControllerEnums.Type type, float direction)
    {
        if(direction == 0) return;
        if (type == ControllerEnums.Type.DpadY)
        {
            _playersToAttack[player] = direction >= 0.9f ? 2 : 1;
        }
        else if (type == ControllerEnums.Type.DpadX)
        {
            if (player == 0){ _playersToAttack[player] = direction >= 0.9f ? 3 : 0;}
            if (player == 1){ _playersToAttack[player] = direction >= 0.9f ? 0 : 3;}
        }
        _selector.ChangeSelectedPos(player, _charactersOnTheField[player == 0 ? 1 : 0, _playersToAttack[player]].GetTransform());
    }
}

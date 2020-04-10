using UnityEngine;

public class MultiplayerCharacterHolder : MonoBehaviour
{
    // =============================================================================================== Private variables
    public CharacterHolder[,] _selectedCharacters = new CharacterHolder[2,4];
    
    // =========================================================================================================== Awake
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // =================================================================================================== Add Character
    /// <summary>
    /// add a character to take to the next scene
    /// </summary>
    /// <param name="player">player 1 or 2?</param>
    /// <param name="chI">charachter slot 1, 2, 3 or 4</param>
    /// <param name="ch">what character</param>
    public void AddCharacter(int player, int chI, CharacterHolder ch)
    {
        _selectedCharacters[player, chI] = ch;
    }
}

using UnityEngine;

public class InBattleSelectorMP : MonoBehaviour
{
    // ============================================================================================= 'Private' variables
    [SerializeField] private FollowTarget[] _selectors;

    // ================================================================================================= Change Position
    public void ChangeSelectedPos(int playerSelector, Transform toTrack)
    {
        _selectors[playerSelector].TARGET = toTrack;
    }
}

using UnityEngine;

public class Enum_Buttons : MonoBehaviour
{
    // the states of which a button can be in
    public enum ButtonState {
        GoingDown,
        IsDown,
        GoingUp };

    // all the buttons 
    public enum Buttons {
        Cross,
        Circle,
        Triangle,
        Square,
        Left_Bumper,
        Right_Bumper,
        Start_Options };

    // axis
    public enum Axis {
        Vertical_L,
        Vertical_R,
        Horizontal_L,
        Horizontal_R,
        Vertical_DPAD,
        Horizontal_DPAD,
        Left_Trigger,
        Right_Trigger };
}

using UnityEngine;

public class XboxGamepadInput : IPlayerInputService
{
    public bool IsJumpPressed => Input.GetKeyDown(KeyCode.Joystick1Button0);

    public bool IsDashPressed => Input.GetKeyDown(KeyCode.Joystick1Button5);

    public bool IsAttackPressed => Input.GetKeyDown(KeyCode.Joystick1Button2);

    public bool IsAttack1Pressed => Input.GetKeyDown(KeyCode.Joystick1Button3);

    public bool IsAttack2Pressed => Input.GetKeyDown(KeyCode.Joystick1Button1);
    public bool IsInteractPressed => throw new System.NotImplementedException();

    public Vector2 MovementInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

}

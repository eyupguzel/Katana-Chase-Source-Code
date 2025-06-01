using UnityEngine;

public class KeyboardInputService : IPlayerInputService
{
    public bool IsJumpPressed => Input.GetKeyDown(KeyCode.W);
    public bool IsDashPressed => Input.GetKeyDown(KeyCode.LeftShift);
    public bool IsAttackPressed => Input.GetMouseButtonDown(0);
    public bool IsAttack1Pressed => Input.GetMouseButtonDown(1);
    public bool IsAttack2Pressed => Input.GetMouseButtonDown(2);
    public bool IsInteractPressed => Input.GetKeyDown(KeyCode.E);

    public Vector2 MovementInput => new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));

}

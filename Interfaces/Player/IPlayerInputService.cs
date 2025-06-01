using UnityEngine;

public interface IPlayerInputService
{
    bool IsJumpPressed { get; }
    bool IsDashPressed { get; }
    bool IsAttackPressed { get; }
    bool IsAttack1Pressed { get; }
    bool IsAttack2Pressed { get; }
    bool IsInteractPressed { get; }
    Vector2 MovementInput { get; }

}

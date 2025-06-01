using UnityEngine;

public class IdleState : IPlayerState,IUpdatableState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;
    public IdleState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService playerInputService)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        input = playerInputService;
    }
    public void Enter()
    {
        EventBus<PlayerStateEvent>.Publish(new PlayerStateEvent
        {
            StateType = PlayerStateType.Idle
        });
        animator.SetTrigger("Idle");
    }

    public void Update()
    {
        if (input.MovementInput.x != 0 && Mathf.Abs(rb.linearVelocity.y) < 0.01f && Mathf.Abs(rb.linearVelocity.x) < 0.01f)
        {
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
        }
        if (input.IsJumpPressed && CheckCollider.Instance.IsGround(rb.transform))
        {
            playerStateMachine.ChangeState(new JumpState(true, rb, animator, playerStateMachine, input));
        }
        if (!CheckCollider.Instance.IsGround(rb.transform))
        {
            playerStateMachine.ChangeState(new JumpState(false,rb, animator, playerStateMachine, input));
        }
    }
    public void Exit()
    {
        animator.ResetTrigger("Idle");
    }

}

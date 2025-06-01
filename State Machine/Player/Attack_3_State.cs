using UnityEngine;

public class Attack_3_State : IPlayerState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;
    public Attack_3_State(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService playerInputService)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        input = playerInputService;
    }
    public void Enter()
    {
        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnAttackAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);
        animator.SetTrigger("Attack_3");
    }
    private void OnAttackAnimationEnd()
    {
        if (rb.linearVelocity.x != 0)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
        else
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }
    public void Exit()
    {
        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerAnimationEndEvent);
        animator.ResetTrigger("Attack_3");
    }
}

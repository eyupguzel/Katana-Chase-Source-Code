using System;
using UnityEngine;

public class AirAttackState : IPlayerState,IUpdatableState
{
    private readonly Animator animator;
    private readonly Rigidbody2D rb;
    private readonly IPlayerInputService input;
    private readonly PlayerStateMachine playerStateMachine;
    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;

    public AirAttackState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.animator = animator;
        this.rb = rb;
        this.playerStateMachine = playerStateMachine;
        this.input = input;
    }
    public void Enter()
    {
        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnPlayerAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);
        animator.SetTrigger("AirAttack");
    }
    public void Update()
    {
        rb.linearVelocityX = input.MovementInput.x * 10f;
    }
    private void OnPlayerAnimationEnd()
    {
        if(input.MovementInput.x != 0)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
        else
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }

    public void Exit()
    {
        animator.ResetTrigger("AirAttack");
        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerAnimationEndEvent);
    }
}

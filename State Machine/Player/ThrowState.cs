using System;
using UnityEngine;

public class ThrowState : IPlayerState,IFixedUpdatableState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;

    private bool oneTime = true;

    public ThrowState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.input = input;

        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnThrowAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);
    }
    public void Enter()
    {
        animator.SetTrigger("Throw");
    }
    public void FixedUpdate()
    {
        if (oneTime)
        {
            rb.linearVelocityX = input.MovementInput.x * 10f;
            oneTime = false;
        }
    }
    private void OnThrowAnimationEnd()
    {
        if(input.MovementInput.x != 0)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
        else
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }
    public void Exit()
    {
        animator.ResetTrigger("Throw");
        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerAnimationEndEvent);
    }
}

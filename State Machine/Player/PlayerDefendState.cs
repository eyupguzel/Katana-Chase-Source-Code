using System;
using UnityEngine;

public class PlayerDefendState : IPlayerState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;
    public PlayerDefendState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.input = input;
    }
    public void Enter()
    {
        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnDefendAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);

        animator.SetTrigger("Defend");
    }

    private void OnDefendAnimationEnd()
    {
        playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }

    public void Exit()
    {
        animator.ResetTrigger("Defend");
    }
}

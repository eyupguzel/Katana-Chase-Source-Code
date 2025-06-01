using System;
using UnityEngine;

public class HealState : IPlayerState,IUpdatableState
{
    private Animator animator;
    private PlayerStateMachine playerStateMachine;
    private Rigidbody2D rb;
    private IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;
    public HealState(Animator animator, PlayerStateMachine playerStateMachine, Rigidbody2D rb, IPlayerInputService input)
    {
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.rb = rb;
        this.input = input;
    }
    public void Enter()
    {
        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);
        animator.SetTrigger("Heal");
    }
    public void Update()
    {
        if (input.MovementInput.x != 0)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
    }
    private void OnAnimationEnd(OnPlayerAnimationEndEvent @event)
    {
        playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }

    public void Exit()
    {
        animator.ResetTrigger("Heal");
        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerAnimationEndEvent);

    }
}

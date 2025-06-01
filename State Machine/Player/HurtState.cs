using UnityEngine;

public class HurtState : IPlayerState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerTakeDamage;

    public HurtState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.input = input;
    }

    public void Enter()
    {
        onPlayerTakeDamage = new EventBinding<OnPlayerAnimationEndEvent>(OnPlayerTakeDamage);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerTakeDamage);
        animator.SetTrigger("Hurt");
    }
    private void OnPlayerTakeDamage()
    {
      if(rb.linearVelocity.x != 0)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
      else
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }
    public void Exit()
    {
        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerTakeDamage);
        animator.ResetTrigger("Hurt");
    }
}

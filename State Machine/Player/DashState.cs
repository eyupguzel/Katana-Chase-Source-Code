using UnityEngine;

public class DashState : IPlayerState, IFixedUpdatableState
{
    private PlayerStateMachine playerStateMachine;
    private Rigidbody2D rb;
    private Animator animator;
    private IPlayerInputService input;

    private EventBinding<OnPlayerAnimationEndEvent> onPlayerAnimationEndEvent;

    public DashState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.input = input;
        this.playerStateMachine = playerStateMachine;
        this.rb = rb;
        this.animator = animator;
    }

    public void Enter()
    {
        EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
        {
            SoundType = PlayerSoundType.Dash
        });
        onPlayerAnimationEndEvent = new EventBinding<OnPlayerAnimationEndEvent>(OnDashAnimationEnd);
        EventBus<OnPlayerAnimationEndEvent>.Subscribe(onPlayerAnimationEndEvent);
        animator.SetTrigger("Dash");

        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }
    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input.MovementInput.x * 20f, rb.linearVelocity.y);
    }
    private void OnDashAnimationEnd()
    {
        if (rb.linearVelocity.x != 0 &&  Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
        else if(Mathf.Abs(rb.linearVelocity.y) > 0.01f)
            playerStateMachine.ChangeState(new JumpState(false,rb, animator, playerStateMachine, input));
        else
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
    }
    public void Exit()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        EventBus<OnPlayerAnimationEndEvent>.Unsubscribe(onPlayerAnimationEndEvent);
        animator.ResetTrigger("Dash");
    }
}

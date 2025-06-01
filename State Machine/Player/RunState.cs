using Unity.VisualScripting;
using UnityEngine;

public class RunState : IPlayerState, IFixedUpdatableState, IUpdatableState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private float timer;
    private const float timeToRun = 0.2f;
    public RunState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService playerInputService)
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
            StateType = PlayerStateType.Run
        });
        animator.SetTrigger("Run");
    }
    public void Update()
    {
        if(input.MovementInput.x == 0)
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
        if (input.IsJumpPressed && CheckCollider.Instance.IsGround(rb.transform))
        {
            playerStateMachine.ChangeState(new JumpState(true, rb, animator, playerStateMachine, input));
        }
        else if(!CheckCollider.Instance.IsGround(rb.transform))
        {
            playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
        }
    }
    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= timeToRun)
        {
            EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
            {
                SoundType = PlayerSoundType.Run
            });
            timer = 0;
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.x = input.MovementInput.x * 10f;
        rb.linearVelocity = velocity;

    }
    public void Exit()
    {
        animator.ResetTrigger("Run");
    }
}

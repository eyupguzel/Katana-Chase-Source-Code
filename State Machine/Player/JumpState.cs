using System;
using UnityEngine;

public class JumpState : IPlayerState, IFixedUpdatableState, IUpdatableState
{
    private float timer;
    private float wallTimer = 1f;

    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    private bool oneTime = true;

    private bool forceActive;

    public JumpState(bool forceActive,Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.forceActive = forceActive;
        this.input = input;
    }
    public void Enter()
    {
        animator.SetTrigger("Jump");

        if (forceActive)
        {
            EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
            {
                SoundType = PlayerSoundType.Jump
            });
        }
    }
    
    public void Update()
    {
        if (input.IsAttackPressed)
            playerStateMachine.ChangeState(new AirAttackState(rb, animator, playerStateMachine, input));

        if (input.MovementInput.x != 0)
        {
            timer += Time.deltaTime;
                if (CheckCollider.Instance.IsWall((int)input.MovementInput.x,rb.transform) && timer < wallTimer)
                {
                    playerStateMachine.ChangeState(new WallSlideState(rb, animator, playerStateMachine, input));
                    timer = 0;
                }
        }

    }
    public void FixedUpdate()
    {
        if (oneTime && forceActive)
        {
            rb.linearVelocityY = 50f;
            oneTime = false;
        }

        if (oneTime && CheckCollider.Instance.IsGround(rb.transform))
            rb.linearVelocityY = 50f;

        rb.linearVelocityX = input.MovementInput.x * 10f;
        animator.SetFloat("BlendY", rb.linearVelocity.y);
        oneTime = false;

        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            if (rb.linearVelocity.x != 0 && input.MovementInput.x != 0)
                playerStateMachine.ChangeState(new RunState(rb, animator, playerStateMachine, input));
            else if(Mathf.Abs(rb.linearVelocity.y) < 0.01f && input.MovementInput.x == 0) 
                playerStateMachine.ChangeState(new IdleState(rb, animator, playerStateMachine, input));
        }
    }
    public void Exit()
    {
        animator.ResetTrigger("Jump");
        if (forceActive)
        {
            EventBus<PlayerAudioEvent>.Publish(new PlayerAudioEvent
            {
                SoundType = PlayerSoundType.Land
            });
        }
    }
}

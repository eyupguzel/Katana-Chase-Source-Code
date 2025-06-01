using UnityEngine;

public class DefendState : IPlayerState
{
    private readonly Rigidbody2D rb;
    private readonly Animator animator;
    private readonly PlayerStateMachine playerStateMachine;
    private readonly IPlayerInputService input;

    public DefendState(Rigidbody2D rb, Animator animator, PlayerStateMachine playerStateMachine, IPlayerInputService input)
    {
        this.rb = rb;
        this.animator = animator;
        this.playerStateMachine = playerStateMachine;
        this.input = input;
    }
    public void Enter()
    {
       animator.SetTrigger("Defend");
    }

    public void Exit()
    {
        animator.ResetTrigger("Defend");
    }
}

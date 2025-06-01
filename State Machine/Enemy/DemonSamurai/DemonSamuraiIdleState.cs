using UnityEngine;

public class DemonSamuraiIdleState : IEnemyState, IWalkableEnemy
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine stateMachine;

    private Vector2 lastPosition, currentPosition;

    public DemonSamuraiIdleState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.stateMachine = stateMachine;

    }
    public void Enter()
    {
        lastPosition = enemy.Position;
        animator.SetTrigger("Idle");
    }
    public void Walk()
    {
        currentPosition = enemy.Position;

        float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;
        if(speed > 0.01f)
            stateMachine.ChangeEnemyState(new DemonSamuraiMovementState(enemy, animator, stateMachine));

        lastPosition = currentPosition;
    }

    public void Exit()
    {
        animator.ResetTrigger("Idle");
    }
}

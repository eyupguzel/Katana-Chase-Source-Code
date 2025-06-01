using UnityEngine;

public class DemonSamuraiMovementState : IEnemyState, IWalkableEnemy
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine stateMachine;

    private Vector2 lastPosition, currentPosition;

    public DemonSamuraiMovementState(IEnemy enemy, Animator animator, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }
    public void Enter()
    {
        lastPosition = enemy.Position;
        animator.SetTrigger("Move");
    }
    public void Walk()
    {
        currentPosition = enemy.Position;
        float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;
        if (speed < 0.01f)
            stateMachine.ChangeEnemyState(new DemonSamuraiIdleState(enemy, animator, stateMachine));

        lastPosition = currentPosition;
    }

    public void Exit()
    {
        animator.ResetTrigger("Move");
    }
}

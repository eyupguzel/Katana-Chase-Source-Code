using UnityEngine;

public class EnemyMovementState : IEnemyState, IWalkableEnemy
{
    private readonly EnemyStateMachine enemyStateMachine;
    private readonly Animator animator;
    private readonly IEnemy enemy;

    private Vector2 lastPosition, currentPosition;
    public EnemyMovementState(IEnemy enemy, Animator animator, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        this.animator = animator;
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
            enemyStateMachine.ChangeEnemyState(new EnemyIdleState(enemy, animator, enemyStateMachine));

        lastPosition = currentPosition;
    }
    public void Exit()
    {
        animator.ResetTrigger("Move");
    }
}

using UnityEngine;

public class FlyingDemonIdle : IEnemyState,IFlyableEnemy
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine enemyState;

    private Vector3 lastPosition, currentPosition;

    public FlyingDemonIdle(IEnemy enemy, Animator animator, EnemyStateMachine enemyState)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.enemyState = enemyState;
    }
    public void Enter()
    {
        lastPosition = enemy.Position;
        animator.SetTrigger("Idle");
    }
    public void Fly()
    {
        currentPosition = enemy.Position;
        float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;
        if (speed > 0.01f)
            enemyState.ChangeEnemyState(new FlyingDemonMovementState(enemy, animator, enemyState));

        lastPosition = currentPosition;
    }
    public void Exit()
    {
        animator.ResetTrigger("Idle");
    }
}

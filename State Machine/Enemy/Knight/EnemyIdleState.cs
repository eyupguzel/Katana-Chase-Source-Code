using UnityEngine;

public class EnemyIdleState : IEnemyState,IWalkableEnemy
{
    private Animator animator;
    private EnemyStateMachine enemyStateMachine;
    private IEnemy enemy;

    private float speedThreshold = 0.01f;
    private Vector3 lastPosition, currentPosition;
    public EnemyIdleState(IEnemy enemy,Animator animator,EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
        this.animator = animator;
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
        if (speed > speedThreshold)
            enemyStateMachine.ChangeEnemyState(new EnemyMovementState(enemy, animator, enemyStateMachine));

        lastPosition = currentPosition;
    }
    public void Exit()
    {
        animator.ResetTrigger("Idle");
    }
}

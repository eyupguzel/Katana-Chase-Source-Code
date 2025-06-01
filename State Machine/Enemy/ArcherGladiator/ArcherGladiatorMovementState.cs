using UnityEngine;

public class ArcherGladiatorMovementState : IEnemyState,IWalkableEnemy
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine enemyStateMachine;

    private Vector2 lastPosition, currentPosition;

    public ArcherGladiatorMovementState(IEnemy enemy, Animator animator, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.enemyStateMachine = enemyStateMachine;
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
        
        if(speed < 0.01f)
            enemyStateMachine.ChangeEnemyState(new ArcherGladiatorIdleState(enemy, animator, enemyStateMachine));

        lastPosition = currentPosition;
    }
    public void Exit()
    {
        animator.ResetTrigger("Move");
    }
}

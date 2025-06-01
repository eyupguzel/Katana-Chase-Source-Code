using UnityEngine;

public class FlyingDemonMovementState : IEnemyState,IFlyableEnemy
{
    private IEnemy enemy;
    private Animator animator;
    private EnemyStateMachine enemyState;

    private Vector3 lastPosition, currentPosition;
    public FlyingDemonMovementState(IEnemy enemy, Animator animator, EnemyStateMachine enemyState)
    {
        this.enemy = enemy;
        this.animator = animator;
        this.enemyState = enemyState;
    }
    public void Enter()
    {
        lastPosition = enemy.Position;
        animator.SetTrigger("Fly");
    }
    public void Fly()
    {
        currentPosition = enemy.Position - lastPosition;
        float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;
        if(speed < 0.01f)
            enemyState.ChangeEnemyState(new FlyingDemonIdle(enemy, animator, enemyState));

        lastPosition = currentPosition;
    }
    public void Exit()
    {
        animator.ResetTrigger("Fly");
    }

   
}
